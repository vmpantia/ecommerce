//Services
import { getClient } from '../services/UserService'

const Layout = () => {
    let client = getClient();
    return (
        <div>{client.email}</div>
    )
}

export default Layout