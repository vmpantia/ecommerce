import axios from 'axios'

//Utilities
import { BASE_URL } from '../utils/Constants'

const axiosAPI = axios.create({
    baseURL : BASE_URL,
    headers : {
        'Content-Type':'application/json',
        Accept: 'application/json',
    }
})

export default axiosAPI