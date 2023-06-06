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
    const[confirmPassword, setConfirmPassword] = useState(STRING_EMPTY);
    const[errors, setErrors] = useState([] as string[]);
    
    const onValueChange = (e:any) => {
        setUser(data => {
            return {...data, [e.target.name] : e.target.value}
        });
    }

    const onButtonClick = () => {
        const tmp = [];
        tmp.push("weak ka");
        tmp.push("weak mo");

        setErrors(tmp);
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
                                errors={errors}
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
                
                <section className='w-full flex justify-end mt-4'>
                    <button className='py-1.5 px-4 mr-2 text-sm bg-blue-600 rounded text-white' onClick={onButtonClick}>Register</button>
                    <button className='py-1.5 px-4 text-sm bg-red-600 rounded text-white' onClick={onButtonClick}>Back</button>
                </section>
            </section>
        </div>
    )
}

export default Register