import axios from 'axios'

const axiosAPI = axios.create({
    baseURL : "localhost",
    headers : {
        'Content-Type':'application/json',
        Accept: 'application/json',
    }
})

export default axiosAPI