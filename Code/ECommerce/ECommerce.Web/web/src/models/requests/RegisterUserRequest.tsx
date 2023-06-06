import { UserDTO } from "../dtos/UserDTO";

export interface RegisterUserRequest {
    inputUser:UserDTO;
    confirmPassword:string;
}