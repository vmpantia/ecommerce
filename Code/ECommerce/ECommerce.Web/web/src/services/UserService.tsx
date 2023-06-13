import axiosAPI from '../api/axiosAPI';

//Models
import { LoginUserRequest } from "../models/requests/LoginUserRequest";
import { ClientDTO } from '../models/dtos/ClientDTO';

//Utilities
import { LOGIN_URL } from "../utils/Constants";

export const loginUserAsync = async (request:LoginUserRequest) => {
    await axiosAPI
    .post(LOGIN_URL, JSON.stringify(request))
    .then(res => { //Response
        if(res.status === 200) {
            //Save clientInfo in session storage
            sessionStorage.setItem("clientInformation", JSON.stringify(res.data)); 
        }
    })
    .catch(err => { //Error
        throw new Error(err);
    })
}

export const logoutUser = () => {
    //Remove clientInformation in session storage
    sessionStorage.clear();
}

export const isAuthenticated = ():boolean => {
    // Check if the clientInformation exists in local and session storage or cookies
    const token = sessionStorage.getItem("clientInformation");
    return token !== null;
}
export const getClient = ():ClientDTO => {
    let client = new ClientDTO();

    //Check if Authenticated
    if(isAuthenticated())
        client =  JSON.parse(sessionStorage.getItem("clientInformation") as string);

    return client;
}