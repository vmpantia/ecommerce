import { useEffect, useState } from "react"

//Properties
import { InputFieldProps } from "../models/props/InputFieldProps"

//Utilities
import { STRING_EMPTY } from "../utils/Constants"

const InputField = ({type, placeholder, required, name, label, value, errorMessage, isDisabled, onValueChangedHandler}:InputFieldProps) => {

    //defaultInputStyle: Use to set default style or className of input
    let defaultInputStyle = "w-full px-2 py-1.5 my-2 border rounded disabled:bg-gray-100 focus:outline-none "

    //inputStyle: Use to change the style or className of input
    const [inputStyle, setInputStyle] = useState(defaultInputStyle)

    //Run function below once errorMessage changed
    useEffect(() => {
        if(errorMessage === undefined || errorMessage === STRING_EMPTY)
            setInputStyle(defaultInputStyle + "focus:border-blue-500 focus:ring-1 ring-blue-400")
        else 
            setInputStyle(defaultInputStyle + "border-red-500 focus:ring-1 ring-red-400")
    }, [errorMessage])

    return (
        <div>
            {/* Input Label */}
            <label className='flex'>
                {required && <p className="text-red-500 font-medium mr-1">*</p>}
                {label}:
            </label>

            {/* Input */}
            <input className={inputStyle}
                    type={type} 
                    placeholder={placeholder}
                    name={name}
                    value={value}
                    disabled={isDisabled}
                    onChange={onValueChangedHandler} />

            {/* Input Error Message */}
            {errorMessage && 
                <span className="float-right px-1.5 py-0.5 bg-red-500 text-xs text-white rounded">
                    {errorMessage}
                </span>
            }
        </div>
    )
}

export default InputField