import { api } from "../api";
import { INote } from "../models/note-model";

export const noteEndpoints = api.injectEndpoints({
    endpoints: (builder)=>({
        getSectionsNotes: builder.query<INote[], {boardId:number; sectionId:number}>({
            query: (noteInfo)=>{
                return `boards/${noteInfo.boardId}/sections/${noteInfo.sectionId}/notes`;
            },
            providesTags:result=>['Notes']
        }),
        createNote: builder.mutation<INote, {boardId:number; sectionId:number; name:string; description:string}>({
            query: (createInfo) => ({
                url:`/boards/${createInfo.boardId}/sections/${createInfo.sectionId}/notes`,
                method: 'POST',
                body: {
                    name: createInfo.name,
                    description: createInfo.description
                }
            }),
            invalidatesTags:['Notes']
        }),
        deleteNote: builder.mutation<INote, {boardId:number; sectionId:number; noteId:number}>({
            query: (deleteInfo) => ({
                url:`/boards/${deleteInfo.boardId}/sections/${deleteInfo.sectionId}/notes/${deleteInfo.noteId}`,
                method: 'DELETE',
            }),
            invalidatesTags:['Notes']
        }),
        updateNote: builder.mutation<INote, {boardId:number; sectionId:number; noteId:number; updateName:string; updateData: string | number }>({
            query: (updateInfo) => {
                return (
                {
                url:`/boards/${updateInfo.boardId}/sections/${updateInfo.sectionId}/notes/${updateInfo.noteId}`,
                method: 'PUT',
                body:{
                    [updateInfo.updateName]: updateInfo.updateData
                }
            })},
            invalidatesTags:['Notes']
        })
    })
})