import React from 'react'

const Login = () => {
    return (

        <div className='w-full h-screen flex justify-center items-center'>
            <section className='p-5 border rounded w-96'>
                <header className='w-full text-xl font-medium pb-2'>
                    Login your Account
                </header>
                <div>
                    <label className='text-sm'>Logon Name:</label>
                    <input className='w-full px-2 py-1.5 my-2 border rounded' type='text' placeholder='Enter your username or email' />
                </div>
                <div>
                    <label className='text-sm'>Password:</label>
                    <input className='w-full px-2 py-1.5 my-2 border rounded'type='passowrd' placeholder='Enter your password' />
                </div>
                
                <div className='mt-4'>
                    <button className='w-full bg-blue-500'>Login</button>
                    <div className='py-2 text-center'>or</div>
                    <button className='w-full bg-blue-500'>Register</button>
                </div>
            </section>
        </div>
    )
}

export default Login