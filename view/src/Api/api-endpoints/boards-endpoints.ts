import { api } from "../api";
import { IBoard } from "../models/board-model";

export const boardsEndpoints = api.injectEndpoints({
    endpoints: (builder)=>({
        getUserBoards: builder.query<IBoard[], number>({
            query: (id)=>{
                return `boards?&userId=${id}`;
            },
            providesTags:result=>['Boards']
        }),
        getBoard: builder.query<IBoard, number>({
            query:(id)=>{
                return `boards/${id}`;
            },
            providesTags:result=>['Boards']
        }),
        createBoard: builder.mutation<IBoard, {userId:number; name:string; description:string}>({
            query: (boardInfo) => ({
                url:'/boards',
                method: 'POST',
                body: boardInfo
            }),
            invalidatesTags:['Boards']
        }),
        updateBoard: builder.mutation<IBoard, {boardId:number; updateName:string; updateData:string|number}>({
            query: (updateData) => ({
                url:`/boards/${updateData.boardId}`,
                method: 'PUT',
                body: {
                    [updateData.updateName]:updateData.updateData
                }
            }),
            invalidatesTags:['Boards']
        }),
    })
})