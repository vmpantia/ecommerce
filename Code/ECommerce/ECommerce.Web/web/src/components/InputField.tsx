import { useEffect, useState } from "react"

//Properties
import { InputFieldProps } from "../models/props/InputFieldProps"

const InputField = ({type, placeholder, required, name, label, value, errors, onValueChangedHandler}:InputFieldProps) => {
    let defaultInputStyle = "w-full px-2 py-1.5 my-2 border rounded focus:outline-none"
    const [inputStyle, setInputStyle] = useState(defaultInputStyle)

    useEffect(() => {
        if(errors === undefined || errors.length === 0)
            setInputStyle(defaultInputStyle + " focus:border-blue-500 focus:ring-1 ring-blue-400")
        else 
            setInputStyle(defaultInputStyle + " border-red-500 focus:ring-1 ring-red-400")
    }, [errors])

    return (
        <div className="text-sm">
            <label className='flex'>
                {required && <p className="text-red-500 font-medium mr-1">*</p>}
                {label}:
            </label>
            <input className={inputStyle}
                    type={type} 
                    placeholder={placeholder}
                    name={name}
                    value={value}
                    onChange={onValueChangedHandler} />
            {errors && 
                errors.map((message, idx) => 
                    (
                        <div key={idx}>
                            <span className="float-right px-1.5 py-0.5 mr-2 bg-red-500 text-xs text-white rounded">
                                {message}
                            </span>
                        </div>
                    )
                )}
        </div>
    )
}

export default InputField