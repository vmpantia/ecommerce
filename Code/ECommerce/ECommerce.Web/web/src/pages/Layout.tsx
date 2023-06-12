//Services
import { UserIcon } from '@heroicons/react/24/solid';
import { getClient } from '../services/UserService'

const Layout = () => {
    let client = getClient();
    return (
        <div>
            <header className='w-full h-16 px-5  bg-gray-800 flex justify-between place-items-center'>
                {/* Company Logo */}
                <section>
                </section>
                {/* Client Details */}
                <section className='inline-flex text-white text-sm'>
                    <div className='mr-4'>
                        <div className='w-full'>{client.email}</div>
                        <div className='w-full text-right font-medium'>{client.name}</div>
                    </div>
                    <UserIcon className='h-8 border rounded-full mt-1.5'/>
                </section>
            </header>
        </div>
    )
}

export default Layout