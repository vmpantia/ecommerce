//Utilities
import { STRING_EMPTY } from "../../utils/Constants";

//Models
import { UserDTO } from "../dtos/UserDTO";

export class RegisterUserRequest {
    inputUser:UserDTO = new UserDTO();
    confirmPassword:string = STRING_EMPTY;
}