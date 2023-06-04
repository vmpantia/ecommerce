import { useState } from "react"

//Icons
import { UserPlusIcon } from "@heroicons/react/24/solid"

//Models
import { UserDTO } from "../models/dtos/UserDTO"

//Components
import InputField from "../components/InputField";

//Constants
import { STRING_EMPTY } from "../utils/Constants";

const Register = () => {
    const[user, setUser] = useState({} as UserDTO);
    const[confirmPassword, setConfirmPassword] = useState(STRING_EMPTY)
    
    const onValueChange = (e:any) => {
        setUser(data => {
            return {...data, [e.target.name]:e.target.value}
        });
    }

    return (
        <div className='w-full h-screen flex justify-center items-center bg-gray-100'>
            <section className='p-9 border rounded w-600 bg-white'>
                <header className='w-full flex text-2xl font-medium pb-3 mb-2 border-b'>
                    <UserPlusIcon className='w-6 mr-2 mt-1'/>
                    Register Account
                </header>
                
                <section className="grid sm:grid-cols-1 md:grid-cols-2 gap-6">
                    <div>
                        <InputField type="text" 
                                    placeholder="Enter your username" 
                                    value={user.userName}
                                    name="username"
                                    label="Username"
                                    onValueChangedHandler={onValueChange} />
                        <InputField type="email" 
                                    placeholder="Enter your email" 
                                    value={user.userName}
                                    name="email"
                                    label="Email"
                                    onValueChangedHandler={onValueChange} />
                        <InputField type="password" 
                                    placeholder="Enter your password" 
                                    value={user.userName}
                                    name="password"
                                    label="Password"
                                    onValueChangedHandler={onValueChange} />
                        <InputField type="password" 
                                    placeholder="Enter your password" 
                                    value={confirmPassword}
                                    name="password"
                                    label="Confirm Password"
                                    onValueChangedHandler={(e) => setConfirmPassword(e.target.value)} />
                    </div>
                    <div>
                        <InputField type="text" 
                                    placeholder="Enter your first name" 
                                    value={user.userName}
                                    name="firstName"
                                    label="First Name"
                                    onValueChangedHandler={onValueChange} />
                        <InputField type="text" 
                                    placeholder="Enter your middle name" 
                                    value={user.userName}
                                    name="middleName"
                                    label="Middle Name"
                                    onValueChangedHandler={onValueChange} />
                        <InputField type="text" 
                                    placeholder="Enter your last name" 
                                    value={user.lastName}
                                    name="lastName"
                                    label="Last Name"
                                    onValueChangedHandler={onValueChange} />
                        <InputField type="date" 
                                    value={user.birthDate}
                                    name="birthDate"
                                    label="Birthdate"
                                    onValueChangedHandler={onValueChange} />
                    </div>
                </section>
                
                <div className='mt-4'>
                    <button className='w-full p-2 text-sm bg-blue-600 rounded text-white'>Register</button>
                </div>
            </section>
        </div>
    )
}

export default Register