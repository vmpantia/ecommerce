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
        <div className='flex justify-center'>
            <section className='w-1/2 p-9 mt-16 border rounded bg-white drop-shadow-xl '>
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
                                onValueChangedHandler={onValueChange} />
                    <InputField type="email" 
                                placeholder="Enter your email" 
                                required={true}
                                name="email"
                                label="Email"
                                value={user.email}
                                onValueChangedHandler={onValueChange} />
                    <InputField type="password" 
                                placeholder="Enter your password" 
                                required={true}
                                name="password"
                                label="Password"
                                value={user.password}
                                onValueChangedHandler={onValueChange} />
                    <InputField type="password" 
                                placeholder="Enter your password" 
                                required={true}
                                name="password"
                                label="Confirm Password"
                                value={confirmPassword}
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
                                onValueChangedHandler={onValueChange} />
                    <InputField type="text" 
                                placeholder="Enter your middle name" 
                                name="middleName"
                                label="Middle Name"
                                value={user.middleName}
                                onValueChangedHandler={onValueChange} />
                    <InputField type="text" 
                                placeholder="Enter your last name" 
                                required={true}
                                name="lastName"
                                label="Last Name"
                                value={user.lastName}
                                onValueChangedHandler={onValueChange} />
                    <InputField type="date" 
                                required={true}
                                name="birthDate"
                                label="Birthdate"
                                value={user.birthDate}
                                onValueChangedHandler={onValueChange} />
                </section>
                
                <div className='mt-4'>
                    <button className='w-full p-2 text-sm bg-blue-600 rounded text-white'>Register</button>
                </div>
            </section>
        </div>
    )
}

export default Register