import React from 'react'
import { Navigate, useLocation } from 'react-router-dom';

//Properties
import { PrivateRouteProps } from '../models/props/PrivateRouteProps';

//Services
import { isAuthenticated } from '../services/UserService';

const PrivateRoute:React.FC<PrivateRouteProps> = ({ children }) => {
    const location = useLocation();
    return isAuthenticated() ? 
    (<>{children}</>) 
    :
    (<Navigate replace={true} 
                to="/login"
                state={{ from: `${location.pathname}${location.search}` }} />)
}

export default PrivateRoute