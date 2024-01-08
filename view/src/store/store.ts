import { combineReducers, configureStore } from "@reduxjs/toolkit";
import { userSlice } from "./slices/user-slice";
import { api } from "../Api/api";

const rootReducer = combineReducers({
    user: userSlice.reducer,
    [api.reducerPath]: api.reducer
})

export const store = configureStore({
    reducer: rootReducer,
    middleware:
        (getdefaultMiddleware)=>
            getdefaultMiddleware().concat(api.middleware)
})

export type RootState = ReturnType<typeof rootReducer>