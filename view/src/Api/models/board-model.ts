import { IElement } from "./element-model";
import { IUser } from "./user-model";

export interface IBoard extends IElement{
    users:IUser[]
}