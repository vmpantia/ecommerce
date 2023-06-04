import { useState } from 'react'

//Icons
import { LockClosedIcon } from '@heroicons/react/24/solid'

//Constants
import { STRING_EMPTY } from '../utils/Constants';

const Login = () => {
    const [logonName, setLogonName] = useState(STRING_EMPTY);
    const [password, setPassword] = useState(STRING_EMPTY);

    return (
        <div className='w-full h-screen flex justify-center items-center bg-gray-100'>
            <section className='p-9 border rounded w-96 bg-white'>
                <header className='w-full flex text-2xl font-medium pb-5'>
                    <LockClosedIcon className='w-6 mr-2 mt-1'/>
                    Login your Account
                </header>
                <div>
                    <label className='text-sm font-medium'>Logon Name:</label>
                    <input className='w-full px-2 py-1.5 my-2 border rounded' 
                            type='text' 
                            placeholder='Enter your username or email'
                            name='logonName'
                            value={logonName}
                            onChange={(e) => setLogonName(e.target.value)} />
                </div>
                <div>
                    <label className='text-sm font-medium'>Password:</label>
                    <input className='w-full px-2 py-1.5 my-2 border rounded' 
                            type='passowrd' 
                            placeholder='Enter your password'
                            name='password'
                            value={password}
                            onChange={(e) => setPassword(e.target.value)} />
                </div>
                <div className='mt-4'>
                    <button className='w-full p-2 text-sm bg-blue-600 rounded text-white'>Login</button>
                    <div className='py-2 text-center'>or</div>
                    <button className='w-full p-2 text-sm bg-blue-600 rounded text-white'>Register</button>
                </div>
            </section>
        </div>
    )
}

export default Login