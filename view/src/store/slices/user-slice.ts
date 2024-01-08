import { createSlice } from "@reduxjs/toolkit";
import { jwtDecode } from "jwt-decode";
import { IToken } from "../../Api/models/token-model";
import { fetchCheckAuth, fetchLogin, fetchRegister } from "../../Api/thunks/auth-thunks";

const fullfilledCase = (state:InitialType, token:string) =>{
    localStorage.setItem('token', `Bearer ${token}`);
    const user: IToken = jwtDecode(token);
    state.user.email = user.email;
    state.user.id = user.id;
    state.user.name = user.name;
    state.isAuth = true;
    state.isLoading = false;
    state.error = '';
}

const pendingCase = (state:InitialType) =>{
    state.isLoading = true;
}

const rejectedCase = (state:InitialType) =>{
    state.isAuth = false;
    state.isLoading = false;
    state.error = 'ошибка авторизации'
}

type InitialType = {
    user:{
        id:number,
        email: string,
        name:string,
    },
    isAuth:boolean;
    isLoading:boolean;
    error:string;
}

const initialState:InitialType = {
    user:{
        id:-1,
        email:'',
        name:''
    },
    isAuth: false,
    isLoading:false,
    error:''
}

export const userSlice = createSlice({
    name:'user',
    initialState,
    reducers:{
        logOut: (state) => {
            localStorage.clear();
            state.user.id = -1;
            state.user.email = '';
            state.user.name = '';
            state.isAuth = false;
        }
    },
    extraReducers: (builder)=>{
        builder
            .addCase(fetchRegister.fulfilled, (state, action)=>{
                fullfilledCase(state, action.payload);
            })
            .addCase(fetchRegister.pending, (state)=>{
                pendingCase(state);
            })
            .addCase(fetchRegister.rejected, (state, action)=>{
                rejectedCase(state);
            })

            .addCase(fetchLogin.fulfilled, (state, action)=>{
                fullfilledCase(state, action.payload);
            })
            .addCase(fetchLogin.pending, (state)=>{
                pendingCase(state);
            })
            .addCase(fetchLogin.rejected, (state, action)=>{
                rejectedCase(state);
            })

            .addCase(fetchCheckAuth.fulfilled, (state, action)=>{
                fullfilledCase(state, action.payload);
            })
    }
})

export const {logOut} = userSlice.actions;