import { useEffect, useState } from 'react'

//Properties
import { DatePickerProps } from '../../models/props/DatePickerProps'

//Utilities
import { INPUT_DEFAULT_STYLE, INPUT_ERROR_MESSAGE_STYLE, INPUT_ERROR_STYLE, INPUT_NORMAL_STYLE, INPUT_REQUIRED_STYLE, STRING_EMPTY } from "../../utils/Constants"

const DatePicker = ({type, required, name, label, value, errorMessage, isDisabled, onValueChangedHandler}:DatePickerProps) => {

    //datePickerStyle: Use to change the style or className of date picker
    const [datePickerStyle, setDatePickerStyle] = useState(INPUT_DEFAULT_STYLE)

    //Run function below once errorMessage changed
    useEffect(() => {
        if(errorMessage === undefined || errorMessage === STRING_EMPTY)
            setDatePickerStyle(INPUT_DEFAULT_STYLE + INPUT_NORMAL_STYLE)
        else 
            setDatePickerStyle(INPUT_DEFAULT_STYLE + INPUT_ERROR_STYLE)
    }, [errorMessage])

    return (
        <div>
            {/* Date Picker Label */}
            <label className='flex'>
                {required && <p className={INPUT_REQUIRED_STYLE}>*</p>}
                {label}:
            </label>

            {/* Date Picker */}
            <input className={datePickerStyle}
                    type={type} 
                    name={name}
                    value={value}
                    disabled={isDisabled}
                    onChange={onValueChangedHandler} />

            {/* Date Picker Error Message */}
            {errorMessage && 
                <span className={INPUT_ERROR_MESSAGE_STYLE}>
                    {errorMessage}
                </span>
            }
        </div>
    )
}

export default DatePicker