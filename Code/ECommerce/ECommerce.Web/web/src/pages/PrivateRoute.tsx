import { Navigate } from 'react-router-dom'

//Services
import { isAuthenticated } from '../services/UserService'

//Properties
import { PrivateRouteProps } from '../models/props/PrivateRouteProps'

const PrivateRoute = ({ children }:PrivateRouteProps) => {
    return isAuthenticated() ? 
    (<>{children}</>)
    :
    (<Navigate to="/login" replace />)
}

export default PrivateRoute