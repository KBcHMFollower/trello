import { api } from "../api";
import { IUser } from "../models/user-model";

export const userEndpoints = api.injectEndpoints({
    endpoints: (builder)=>({
        getOneUser: builder.query<IUser, number>({
            query: (id)=>{
                return `users/${id}`;
            },
            providesTags:result=>['Users']
        }),
        getAllUsers: builder.query<IUser[], string>({
            query: (email)=>{
                return `users?&email=${email}`;
            },
            providesTags:result=>['Users']
        })
    })
})