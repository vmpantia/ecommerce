import { useState } from "react"

//Icons
import { UserPlusIcon } from "@heroicons/react/24/solid"

//Models
import { UserDTO } from "../models/dtos/UserDTO"

//Components
import InputField from "../components/InputField";

//Constants
import { STRING_EMPTY } from "../utils/Constants";
import axiosAPI from "../api/axiosAPI";
import { RegisterUserRequest } from "../models/requests/RegisterUserRequest";

const Register = () => {
    const[user, setUser] = useState({} as UserDTO);
    const[confirmPassword, setConfirmPassword] = useState(STRING_EMPTY);
    const[inputErrors, setInputErrors] = useState();
    
    const onValueChange = (e:any) => {
        setUser(data => {
            return {...data, [e.target.name] : e.target.value}
        });
    }

    const onButtonClick = async () => {
        let request:RegisterUserRequest = { 
            inputUser:user,
            confirmPassword:confirmPassword
        };
        await axiosAPI.post("User/RegisterUser",  /* API Url */
                            JSON.stringify(request) /* Request of Body */
                            )
                            .then(res => {
                                if(res.status === 200)
                                    console.log(res.data);
                            })
                            .catch(err => {
                                if(err.response.data.errors !== null || err.response.data.errors !== undefined)
                                    setInputErrors(err.response.data.errors);
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
                                errors={inputErrors && inputErrors['inputUser.Username']}
                                onValueChangedHandler={onValueChange} />
                    <InputField type="email" 
                                placeholder="Enter your email" 
                                required={true}
                                name="email"
                                label="Email"
                                value={user.email}
                                errors={inputErrors && inputErrors['inputUser.Email']}
                                onValueChangedHandler={onValueChange} />
                    <InputField type="password" 
                                placeholder="Enter your password" 
                                required={true}
                                name="password"
                                label="Password"
                                value={user.password}
                                errors={inputErrors && inputErrors['inputUser.Password']}
                                onValueChangedHandler={onValueChange} />
                    <InputField type="password" 
                                placeholder="Enter your password" 
                                required={true}
                                name="confirmPassword"
                                label="Confirm Password"
                                value={confirmPassword}
                                errors={inputErrors && inputErrors['ConfirmPassword']}
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
                                errors={inputErrors && inputErrors['inputUser.FirstName']}
                                onValueChangedHandler={onValueChange} />
                    <InputField type="text" 
                                placeholder="Enter your middle name" 
                                name="middleName"
                                label="Middle Name"
                                value={user.middleName}
                                errors={inputErrors && inputErrors['inputUser.MiddleName']}
                                onValueChangedHandler={onValueChange} />
                    <InputField type="text" 
                                placeholder="Enter your last name" 
                                required={true}
                                name="lastName"
                                label="Last Name"
                                value={user.lastName}
                                errors={inputErrors && inputErrors['inputUser.LastName']}
                                onValueChangedHandler={onValueChange} />
                    <InputField type="date" 
                                required={true}
                                name="birthDate"
                                label="Birthdate"
                                value={user.birthDate}
                                errors={inputErrors && inputErrors['inputUser.BirthDate']}
                                onValueChangedHandler={onValueChange} />
                </section>
                
                <section className='w-full flex justify-end mt-4'>
                    <button className='py-1.5 px-4 mr-2 text-sm bg-blue-600 rounded text-white' onClick={onButtonClick}>Register</button>
                    <button className='py-1.5 px-4 text-sm bg-red-600 rounded text-white' onClick={onButtonClick}>Back</button>
                </section>
            </section>
        </div>
    )
}

export default Register