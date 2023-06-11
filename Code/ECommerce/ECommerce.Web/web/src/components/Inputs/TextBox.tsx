import { useEffect, useState } from "react"

//Properties
import { TextBoxProps } from "../../models/props/TextBoxProps"

//Utilities
import { INPUTS_DEFAULT_STYLE, INPUTS_ERROR_MESSAGE_STYLE, INPUTS_ERROR_STYLE, INPUTS_NORMAL_STYLE, INPUTS_REQUIRED_STYLE, STRING_EMPTY } from "../../utils/Constants"

const TextBox = ({type, placeholder, required, name, label, value, errorMessage, isDisabled, onValueChangedHandler}:TextBoxProps) => {

    //inputStyle: Use to change the style or className of input
    const [inputStyle, setInputStyle] = useState(INPUTS_DEFAULT_STYLE)

    //Run function below once errorMessage changed
    useEffect(() => {
        if(errorMessage === undefined || errorMessage === STRING_EMPTY)
            setInputStyle(INPUTS_DEFAULT_STYLE + INPUTS_NORMAL_STYLE)
        else 
            setInputStyle(INPUTS_DEFAULT_STYLE + INPUTS_ERROR_STYLE)
    }, [errorMessage])

    return (
        <div>
            {/* Input Label */}
            <label className='flex'>
                {required && <p className={INPUTS_REQUIRED_STYLE}>*</p>}
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
                <span className={INPUTS_ERROR_MESSAGE_STYLE}>
                    {errorMessage}
                </span>
            }
        </div>
    )
}

export default TextBox