import { useState } from 'react'

//Icons
import { LockClosedIcon } from '@heroicons/react/24/solid'

//Components
import TextBox from '../components/Inputs/TextBox';
import ActionButton from '../components/ActionButton';

//Constants
import { STRING_EMPTY } from '../utils/Constants';

const Login = () => {
    const [logonName, setLogonName] = useState(STRING_EMPTY);
    const [password, setPassword] = useState(STRING_EMPTY);

    const onLoginClick = async () => {

    }
    const onRegisterClick = async () => {

    }

    return (
        <div className='w-full h-screen flex justify-center items-center bg-gray-100'>
            <section className='p-9 border rounded w-96 bg-white'>
                <header className='w-full flex text-2xl font-medium pb-5'>
                    <LockClosedIcon className='w-6 mr-2 mt-1'/>
                    Login your Account
                </header>
                <TextBox type="text" 
                                placeholder="Enter your username or email" 
                                required={false}
                                name="logonName"
                                label="Logon Name"
                                value={logonName}
                                //errorMessage={GetErrorByName(inputErrors, "inputUser.Username")} //Error Message Properties
                                isDisabled={false}
                                onValueChangedHandler={(e) => setLogonName(e.target.value)} />
                <TextBox type="password" 
                                placeholder="Enter your password" 
                                required={false}
                                name="password"
                                label="Password"
                                value={password}
                                //errorMessage={GetErrorByName(inputErrors, "inputUser.Username")} //Error Message Properties
                                isDisabled={false}
                                onValueChangedHandler={(e) => setPassword(e.target.value)} />
                <section className='w-full flex justify-end mt-4'>
                    <ActionButton type="primary" 
                                    label="Login"
                                    isDisabled={false} 
                                    onButtonClickedHandler={onLoginClick} />
                    <ActionButton type="info" 
                                    label="Register"
                                    isDisabled={false} 
                                    onButtonClickedHandler={onRegisterClick} />
                </section>
            </section>
        </div>
    )
}

export default Login