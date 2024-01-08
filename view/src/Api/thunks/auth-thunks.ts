import { createAsyncThunk } from "@reduxjs/toolkit";
import { $host } from "../api";
import { AxiosError } from "axios";

export interface ApiError{
    status:number;
    message:string;
}

export const fetchRegister = createAsyncThunk('user/fetchRegister',
async ({email, pass, name}:{email:string;pass:string, name:string}, thunkAPI)=>{
    try {
        const res = await $host.post<string>('api/users/register', {email:email, pass:pass, name:name});
        return res.data;
    } catch (error) {
        const axiosError = error as AxiosError<ApiError>;
        return thunkAPI.rejectWithValue(axiosError.response?.data.message);
    }
})

export const fetchLogin = createAsyncThunk('user/fetchLogin',
async ({email, pass}:{email:string;pass:string}, thunkAPI)=>{
    try {
        const res = await $host.post<string>('api/users/login', {email:email, pass:pass});
        return res.data;
    } catch (error) {
        const axiosError = error as AxiosError<ApiError>;
        return thunkAPI.rejectWithValue(axiosError.response?.data.message);
    }
})

export const fetchCheckAuth = createAsyncThunk('user/fetchAuth',
  async (thunkAPI)=> {
    try {
      const response = await $host.get<string>('api/users/auth');
      return response.data;
    } catch (error) {
      throw new Error('Failed to fetch data');
    }
  })