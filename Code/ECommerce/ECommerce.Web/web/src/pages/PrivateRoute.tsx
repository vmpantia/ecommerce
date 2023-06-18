import React from 'react'
import { isAuthenticated } from '../services/UserService'
import { Navigate, Route } from 'react-router-dom'
import { PrivateRouteProps } from '../models/props/PrivateRouteProps'

const PrivateRoute = ({ children }:PrivateRouteProps) => {
    return isAuthenticated() ? 
    (<>{children}</>)
    :
    (<Navigate to="/login" replace />)
}

export default PrivateRoute