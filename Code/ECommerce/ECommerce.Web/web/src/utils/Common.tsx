import { STRING_EMPTY } from "./Constants"

export const GetErrorByName = (errors:any, name:string) => {
    return errors === undefined ? STRING_EMPTY : errors[name];
}