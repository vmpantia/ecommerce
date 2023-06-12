import { useState } from "react"
import { useNavigate } from 'react-router-dom';
import { toast } from "react-toastify";
import axiosAPI from "../api/axiosAPI";
import { format } from "date-fns";

import { NIL as NIL_UUID } from 'uuid';

//Icons
import { UserPlusIcon } from "@heroicons/react/24/solid"

//Models
import { UserDTO } from "../models/dtos/UserDTO"
import { RegisterUserRequest } from "../models/requests/RegisterUserRequest";

//Components
import TextBox from "../components/Inputs/TextBox";
import DatePicker from "../components/Inputs/DatePicker";
import ActionButton from "../components/Buttons/ActionButton";
import LinkButton from "../components/Buttons/LinkButton";

//Utilities
import { REGISTER_URL, STRING_EMPTY } from "../utils/Constants";
import { GetErrorByName } from "../utils/Common";

const Register = () => {
    const navigate = useNavigate();
    //React Hooks
    const [user, setUser] = useState<UserDTO>({
        internalID: NIL_UUID,
        userName: STRING_EMPTY,
        email: STRING_EMPTY,
        password: STRING_EMPTY,
        role: STRING_EMPTY,
        firstName: STRING_EMPTY,
        middleName: undefined,
        lastName: STRING_EMPTY,
        birthDate: new Date(),
        profile: STRING_EMPTY,
        status: 0,
        statusDescription: STRING_EMPTY,
        createDate: new Date(),
        modifiedDate: undefined,
    });
    const [confirmPassword, setConfirmPassword] = useState(STRING_EMPTY);
    const [inputErrors, setInputErrors] = useState();
    const [loadingState, setLoadingState] = useState(false);
    
    //onInputTextValueChange will execute once the InputTexts value is changed
    //It will set a value in the properties of user hook
    const onInputTextValueChange = (e:any) => {
        setUser(data => {
            return {...data, [e.target.name] : e.target.value}
        });
    }

    //onValueChange will execute once the DatePickers value is changed
    //It will set a value in the properties of user hook
    const onDatePickerValueChange = (e:any) => {
        let selectedDate = new Date(e.target.value);
        if(!isNaN(selectedDate.getTime()))
            setUser(data => {
                return {...data, [e.target.name] : selectedDate }
            });
    }

    //onRegisterClick will execute once the Register button clicked 
    const onRegisterClick = async () => {    
        setInputErrors(undefined); /* Reset Error */
        setLoadingState(true); /* Set Loading State */

        //Set timeout for registering user
        setTimeout(async () => {
            await registerUser();
            setLoadingState(false);
        }, 1000);
    }

    //It will call the User/RegisterUser API to process the request
    const registerUser = async () => {
        let request:RegisterUserRequest = { 
            inputUser:user,
            confirmPassword:confirmPassword
        };
        //Set default value for register user
        request.inputUser.role = "User";
        request.inputUser.status = -1;
        await axiosAPI.post(REGISTER_URL,  /* API Url */
                            JSON.stringify(request) /* Request of Body */
                            )
                            .then(res => {
                                if(res.status === 200) {
                                    toast.success(res.data);
                                    navigate("/login")
                                }
                            })
                            .catch(err => {
                                if(err.response == null) /* API not working */
                                    toast.error(err.message);
                                else if(err.response.data.errors != null) /* Response Error or Validation Required */
                                    setInputErrors(err.response.data.errors);
                                else if(err.response.data != STRING_EMPTY) /* Expected Error */
                                    toast.error(err.response.data);
                                else  /* Unexpected Error */
                                    toast.error("Error in sending a request to API. [Code: " + err.response.status +"]")
                            });
    }

    return (
        <div className='flex justify-center'>
            <section className='w-1/2 p-9 my-10 border rounded bg-white drop-shadow-xl '>
                <header className='w-full flex text-2xl font-medium border-b pb-3'>
                    <UserPlusIcon className='w-6 mr-2 mt-1'/>
                    Register Account
                </header>
                {/* User details */}
                <section className="font-medium pt-3">User Details</section>
                <section className="mt-3 grid md:grid-cols-1 lg:grid-cols-2 gap-3">
                    <TextBox type="text" 
                                placeholder="Enter your username" 
                                required={true}
                                name="userName"
                                label="Username"
                                value={user.userName}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.Username")} //Error Message Properties
                                isDisabled={loadingState}
                                onValueChangedHandler={onInputTextValueChange} />
                    <TextBox type="email" 
                                placeholder="Enter your email" 
                                required={true}
                                name="email"
                                label="Email"
                                value={user.email}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.Email")} //Error Message Properties
                                isDisabled={loadingState}
                                onValueChangedHandler={onInputTextValueChange} />
                    <TextBox type="password" 
                                placeholder="Enter your password" 
                                required={true}
                                name="password"
                                label="Password"
                                value={user.password}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.Password")} //Error Message Properties
                                isDisabled={loadingState}
                                onValueChangedHandler={onInputTextValueChange} />
                    <TextBox type="password" 
                                placeholder="Enter your password" 
                                required={true}
                                name="confirmPassword"
                                label="Confirm Password"
                                value={confirmPassword}
                                errorMessage={GetErrorByName(inputErrors, "ConfirmPassword")} //Error Message Properties
                                isDisabled={loadingState}
                                onValueChangedHandler={(e) => setConfirmPassword(e.target.value)} />
                </section>
                
                {/* Personal details */}
                <section className="font-medium pt-3">Personal Details</section>
                <section className="mt-3 grid md:grid-cols-1 lg:grid-cols-2 gap-3">
                    <TextBox type="text" 
                                placeholder="Enter your first name" 
                                required={true}
                                name="firstName"
                                label="First Name"
                                value={user.firstName}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.FirstName")} //Error Message Properties
                                isDisabled={loadingState}
                                onValueChangedHandler={onInputTextValueChange} />
                    <TextBox type="text" 
                                placeholder="Enter your middle name" 
                                name="middleName"
                                label="Middle Name"
                                value={user.middleName}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.MiddleName")} //Error Message Properties
                                isDisabled={loadingState}
                                onValueChangedHandler={onInputTextValueChange} />
                    <TextBox type="text" 
                                placeholder="Enter your last name" 
                                required={true}
                                name="lastName"
                                label="Last Name"
                                value={user.lastName}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.LastName")} //Error Message Properties
                                isDisabled={loadingState}
                                onValueChangedHandler={onInputTextValueChange} />
                    <DatePicker type="date" 
                                required={true}
                                name="birthDate"
                                label="Birth Date"
                                value={user.birthDate && format(user.birthDate, 'yyy-MM-dd')}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.BirthDate")} //Error Message Properties
                                isDisabled={loadingState}
                                onValueChangedHandler={onDatePickerValueChange} />
                </section>
                
                <section className='w-full flex justify-end mt-4'>
                    <ActionButton type="primary" 
                                    label="Register"
                                    isDisabled={loadingState} 
                                    onButtonClickedHandler={onRegisterClick} />
                </section>
                <section className='w-full flex justify-end mt-2'>
                    <LinkButton label='I already have an account' url='/login' isDisabled={loadingState} />
                </section>
            </section>
        </div>
    )
}

export default Register