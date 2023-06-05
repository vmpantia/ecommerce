import { useEffect, useState } from "react"

//Properties
import { InputFieldProps } from "../models/props/InputFieldProps"

//Constants
import { STRING_EMPTY } from "../utils/Constants"

const InputField = ({type, placeholder, required, name, label, value, onValueChangedHandler}:InputFieldProps) => {
    let defaultInputStyle = "w-full px-2 py-1.5 my-2 border rounded focus:outline-none"

    const [errorMessage, setErrorMessage] = useState(STRING_EMPTY)
    const [inputStyle, setInputStyle] = useState(defaultInputStyle)

    useEffect(() => {
        if(errorMessage !== STRING_EMPTY)
            setInputStyle(defaultInputStyle + " border-red-500 focus:ring-1 ring-red-400")
        else 
            setInputStyle(defaultInputStyle + " focus:border-blue-500 focus:ring-1 ring-blue-400")
    }, [errorMessage])

    const onValueChange = (e:any) => {
        onValueChangedHandler(e);
        //Required Checking
        if(e.target.value === STRING_EMPTY && required)
            setErrorMessage("The " + label +  " field is required.");
        else
            setErrorMessage(STRING_EMPTY);
    }

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
                    onChange={onValueChange} />
            {errorMessage && <span className="float-right px-1.5 py-0.5 bg-red-500 text-xs text-white rounded">{errorMessage}</span>}
        </div>
    )
}

export default InputField