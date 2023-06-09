import { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import axiosAPI from '../api/axiosAPI';
import { toast } from 'react-toastify';

//Icons
import { LockClosedIcon } from '@heroicons/react/24/solid'

//Components
import TextBox from '../components/Inputs/TextBox';
import ActionButton from '../components/Buttons/ActionButton';
import LinkButton from '../components/Buttons/LinkButton';

//Models
import { LoginUserRequest } from '../models/requests/LoginUserRequest';

//Utilities
import { LOGIN_URL, STRING_EMPTY } from '../utils/Constants';
import { GetErrorByName } from '../utils/Common';
import { loginUserAsync } from '../services/UserService';

const Login = () => {
    const navigate = useNavigate();
    //React Hooks
    const [logonName, setLogonName] = useState(STRING_EMPTY);
    const [password, setPassword] = useState(STRING_EMPTY);
    const [loadingState, setLoadingState] = useState(false);
    const [inputErrors, setInputErrors] = useState();

    //onLoginClick will execute once the Login button clicked 
    const onLoginClick = async () => {
        setInputErrors(undefined); /* Reset Error */
        setLoadingState(true); /* Set Loading State */

        //Set timeout for registering user
        setTimeout(async () => {
            await loginUser();
            setLoadingState(false);
        }, 1000);
    }

    //It will call the User/LoginUser API to process the request
    const loginUser = async () => {
        let request:LoginUserRequest = {
            logonName: logonName,
            password: password
        };
        await loginUserAsync(request)
        .then(() => navigate("/")) // Success
        .catch(err => { // Error
            if(err.response == null) /* API not working */
                toast.error(err.message);
            else if(err.response.data.errors != null) /* Response Error or Validation Required */
                setInputErrors(err.response.data.errors);
            else if(err.response.data != STRING_EMPTY) /* Expected Error */
                toast.error(err.response.data);
            else  /* Unexpected Error */
                toast.error("Error in sending a request to API. [Code: " + err.response.status +"]")
        })
    }

    return (
        <div className='fixed inset-0 flex justify-center items-center bg-gray-100'>
            <section className='p-9 border rounded w-96 bg-white'>
                <header className='w-full flex text-2xl font-medium pb-5'>
                    <LockClosedIcon className='w-6 mr-2 mt-1'/>
                    Login your Account
                </header>
                <section className="mt-3 grid grid-cols-1 gap-3">
                    <TextBox type="text" 
                                    placeholder="Enter your username or email" 
                                    required={true}
                                    name="logonName"
                                    label="Logon Name"
                                    value={logonName}
                                    errorMessage={GetErrorByName(inputErrors, "LogonName")} //Error Message Properties
                                    isDisabled={loadingState}
                                    onValueChangedHandler={(e) => setLogonName(e.target.value)} />
                    <TextBox type="password" 
                                    placeholder="Enter your password" 
                                    required={true}
                                    name="password"
                                    label="Password"
                                    value={password}
                                    errorMessage={GetErrorByName(inputErrors, "Password")} //Error Message Properties
                                    isDisabled={loadingState}
                                    onValueChangedHandler={(e) => setPassword(e.target.value)} />
                </section>
                <section className='w-full flex justify-end mt-4'>
                    <ActionButton type="primary" 
                                    label="Login"
                                    isDisabled={loadingState} 
                                    onButtonClickedHandler={onLoginClick} />
                </section>
                <section className='w-full flex justify-end mt-2'>
                    <LinkButton label='Create new account' url='/register' isDisabled={loadingState} />
                </section>
            </section>
        </div>
    )
}

export default Login