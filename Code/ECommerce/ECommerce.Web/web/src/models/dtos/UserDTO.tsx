import { NIL as NIL_UUID } from 'uuid';

//Utilities
import { STRING_EMPTY } from '../../utils/Constants';
export class UserDTO {
    internalID:string = NIL_UUID;
    userName:string = STRING_EMPTY;
    email:string = STRING_EMPTY;
    password:string = STRING_EMPTY;
    role:string = STRING_EMPTY;
    firstName:string = STRING_EMPTY;
    middleName?:string;
    lastName:string = STRING_EMPTY;
    birthDate:string = STRING_EMPTY;
    profile:string = STRING_EMPTY;
    status:number = 0;
    statusDescription?:string;
    createDate:Date = new Date();
    modifiedDate?:Date;
}