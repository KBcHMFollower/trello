import { api } from "../api";
import { IElement } from "../models/element-model";

export const sectionsEndpoints = api.injectEndpoints({
    endpoints: (builder)=>({
        getBoardSections: builder.query<IElement[], number>({
            query: (id)=>{
                return `boards/${id}/sections`;
            },
            providesTags:result=>['Sections']
        }),
        createSection: builder.mutation<IElement, {boardId: number; secName:string}>({
            query: (createInfo) => ({
                url:`/boards/${createInfo.boardId}/sections`,
                method: 'POST',
                body: {
                    name: createInfo.secName
                }
            }),
            invalidatesTags:['Sections']
        }),
        deleteSection: builder.mutation<IElement, {boardId: number; secId:number}>({
            query: (deleteInfo) => ({
                url:`/boards/${deleteInfo.boardId}/sections/${deleteInfo.secId}`,
                method: 'DELETE',
            }),
            invalidatesTags:['Sections']
        }),
        updateSection: builder.mutation<IElement, {boardId:number; sectionId:number; newName: string}>({
            query: (updateInfo) => {
                debugger;
                return (
                {
                url:`/boards/${updateInfo.boardId}/sections/${updateInfo.sectionId}`,
                method: 'PUT',
                body:{
                    name: updateInfo.newName
                }
            })},
            invalidatesTags:['Sections']
        })
    })
})