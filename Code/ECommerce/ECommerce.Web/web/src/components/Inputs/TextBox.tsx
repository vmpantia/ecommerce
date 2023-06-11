import { useEffect, useState } from "react"

//Properties
import { TextBoxProps } from "../../models/props/TextBoxProps"

//Utilities
import { INPUT_DEFAULT_STYLE, INPUT_ERROR_MESSAGE_STYLE, INPUT_ERROR_STYLE, INPUT_NORMAL_STYLE, INPUT_REQUIRED_STYLE, STRING_EMPTY } from "../../utils/Constants"

const TextBox = ({type, placeholder, required, name, label, value, errorMessage, isDisabled, onValueChangedHandler}:TextBoxProps) => {

    //inputStyle: Use to change the style or className of input
    const [inputStyle, setInputStyle] = useState(INPUT_DEFAULT_STYLE)

    //Run function below once errorMessage changed
    useEffect(() => {
        if(errorMessage === undefined || errorMessage === STRING_EMPTY)
            setInputStyle(INPUT_DEFAULT_STYLE + INPUT_NORMAL_STYLE)
        else 
            setInputStyle(INPUT_DEFAULT_STYLE + INPUT_ERROR_STYLE)
    }, [errorMessage])

    return (
        <div>
            {/* Input Label */}
            <label className='flex'>
                {required && <p className={INPUT_REQUIRED_STYLE}>*</p>}
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
                <span className={INPUT_ERROR_MESSAGE_STYLE}>
                    {errorMessage}
                </span>
            }
        </div>
    )
}

export default TextBox