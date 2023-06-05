import { useState } from "react"
import { InputFieldProps } from "../models/props/InputFieldProps"
import { STRING_EMPTY } from "../utils/Constants"
import { error } from "console"

const InputField = ({type, placeholder, required, name, label, value, onValueChangedHandler}:InputFieldProps) => {
    const [errorMessage, setErrorMessage] = useState(STRING_EMPTY)

    const onValueChange = (e:any) => {
        onValueChangedHandler(e);

        //Required Checking
        if(e.target.value === STRING_EMPTY && required) {
            setErrorMessage("The " + label +  " field is required.");
            return;
        }
        setErrorMessage(STRING_EMPTY);
    }

    return (
        <div className="text-sm">
            <label className='flex'>
                {required && <p className="text-red-500 font-medium mr-1">*</p>}
                {label}:
            </label>
            <input className='w-full px-2 py-1.5 my-2 border rounded' 
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