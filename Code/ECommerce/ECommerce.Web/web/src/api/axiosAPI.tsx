import axios from 'axios'

const axiosAPI = axios.create({
    baseURL : "https://localhost:7084/api/",
    headers : {
        'Content-Type':'application/json',
        Accept: 'application/json',
    }
})

export default axiosAPI