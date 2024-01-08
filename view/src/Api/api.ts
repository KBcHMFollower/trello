import axios, { InternalAxiosRequestConfig } from "axios";
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

//axios

export const $host = axios.create({
    baseURL: 'http://localhost:5000/'
})

const hostInterceptor = (config: InternalAxiosRequestConfig): InternalAxiosRequestConfig => {
    if(config.headers){
      config.headers.Authorization = `${localStorage.getItem('token')}`;
    }
    console.log(config);
    return config;
  }
  
  $host.interceptors.request.use(hostInterceptor)

  //rtk

  const baseQuery = fetchBaseQuery({
    baseUrl:'http://localhost:5000/api',
    prepareHeaders(headers, api) {
      headers.set('Authorization', `${localStorage.getItem('token')}`);
      return headers;
    },
  })

  export const api = createApi({
    reducerPath: 'Api',
    baseQuery: baseQuery,
    tagTypes:['Users', 'Boards', 'Sections', 'Notes'],
    endpoints:()=>({})
  })