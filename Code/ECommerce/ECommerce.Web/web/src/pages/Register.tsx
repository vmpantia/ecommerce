import { useState } from "react"
import axiosAPI from "../api/axiosAPI";

//Icons
import { UserPlusIcon } from "@heroicons/react/24/solid"

//Models
import { UserDTO } from "../models/dtos/UserDTO"
import { RegisterUserRequest } from "../models/requests/RegisterUserRequest";

//Components
import InputField from "../components/InputField";

//Utilities
import { REGISTER_URL, STRING_EMPTY } from "../utils/Constants";
import { GetErrorByName } from "../utils/Common";
import ActionButton from "../components/ActionButton";

const Register = () => {
    //React Hooks
    const[user, setUser] = useState({} as UserDTO);
    const[confirmPassword, setConfirmPassword] = useState(STRING_EMPTY);
    const[inputErrors, setInputErrors] = useState();
    
    //onValueChange will execute once the InputFields value is changed
    //It will set a value in the properties of user hook
    const onValueChange = (e:any) => {
        setUser(data => {
            return {...data, [e.target.name] : e.target.value}
        });
    }

    //onRegisterClick will execute once the Register button clicked 
    //It will call the User/RegisterUser API to process the request
    const onRegisterClick = async () => {
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
                                if(res.status === 200)
                                    console.log(res.data);
                            })
                            .catch(err => {
                                if(err.response.data.errors != null) /* Response Error or Validation Required */ 
                                    setInputErrors(err.response.data.errors);
                                else if (err.response.data != STRING_EMPTY) /* Expected Error */
                                    console.log(err.response.data)
                                else /* Unexpected Error */
                                    console.log(err.message);
                            });
    }

    return (
        <div className='flex justify-center'>
            <section className='w-1/2 p-9 my-10 border rounded bg-white drop-shadow-xl '>
                <header className='w-full flex text-2xl font-medium border-b pb-3'>
                    <UserPlusIcon className='w-6 mr-2 mt-1'/>
                    Register Account
                </header>

                <section className="font-medium pt-3">User Details</section>
                <section className="mt-3 grid md:grid-cols-1 lg:grid-cols-2 gap-3">
                    <InputField type="text" 
                                placeholder="Enter your username" 
                                required={true}
                                name="username"
                                label="Username"
                                value={user.userName}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.Username")} //Error Message Properties
                                onValueChangedHandler={onValueChange} />
                    <InputField type="email" 
                                placeholder="Enter your email" 
                                required={true}
                                name="email"
                                label="Email"
                                value={user.email}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.Email")} //Error Message Properties
                                onValueChangedHandler={onValueChange} />
                    <InputField type="password" 
                                placeholder="Enter your password" 
                                required={true}
                                name="password"
                                label="Password"
                                value={user.password}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.Password")} //Error Message Properties
                                onValueChangedHandler={onValueChange} />
                    <InputField type="password" 
                                placeholder="Enter your password" 
                                required={true}
                                name="confirmPassword"
                                label="Confirm Password"
                                value={confirmPassword}
                                errorMessage={GetErrorByName(inputErrors, "ConfirmPassword")} //Error Message Properties
                                onValueChangedHandler={(e) => setConfirmPassword(e.target.value)} />
                </section>
                
                <section className="font-medium pt-3">Personal Details</section>
                <section className="mt-3 grid md:grid-cols-1 lg:grid-cols-2 gap-3">
                    <InputField type="text" 
                                placeholder="Enter your first name" 
                                required={true}
                                name="firstName"
                                label="First Name"
                                value={user.firstName}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.FirstName")} //Error Message Properties
                                onValueChangedHandler={onValueChange} />
                    <InputField type="text" 
                                placeholder="Enter your middle name" 
                                name="middleName"
                                label="Middle Name"
                                value={user.middleName}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.MiddleName")} //Error Message Properties
                                onValueChangedHandler={onValueChange} />
                    <InputField type="text" 
                                placeholder="Enter your last name" 
                                required={true}
                                name="lastName"
                                label="Last Name"
                                value={user.lastName}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.LastName")} //Error Message Properties
                                onValueChangedHandler={onValueChange} />
                    <InputField type="date" 
                                required={true}
                                name="birthDate"
                                label="Birthdate"
                                value={user.birthDate}
                                errorMessage={GetErrorByName(inputErrors, "inputUser.BirthDate")} //Error Message Properties
                                onValueChangedHandler={onValueChange} />
                </section>
                
                <section className='w-full flex justify-end mt-4'>
                    <ActionButton type="primary" label="Register" onButtonClickedHandler={onRegisterClick} />
                    <ActionButton type="secondary" label="Back" onButtonClickedHandler={onRegisterClick} />
                </section>
            </section>
        </div>
    )
}

export default Register