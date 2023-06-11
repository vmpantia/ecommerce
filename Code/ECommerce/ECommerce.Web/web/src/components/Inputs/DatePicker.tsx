import { useEffect, useState } from 'react'

//Properties
import { DatePickerProps } from '../../models/props/DatePickerProps'

//Utilities
import { INPUTS_DEFAULT_STYLE, INPUTS_ERROR_MESSAGE_STYLE, INPUTS_ERROR_STYLE, INPUTS_NORMAL_STYLE, INPUTS_REQUIRED_STYLE, STRING_EMPTY } from "../../utils/Constants"

const DatePicker = ({type, required, name, label, value, errorMessage, isDisabled, onValueChangedHandler}:DatePickerProps) => {

    //datePickerStyle: Use to change the style or className of date picker
    const [datePickerStyle, setDatePickerStyle] = useState(INPUTS_DEFAULT_STYLE)

    //Run function below once errorMessage changed
    useEffect(() => {
        if(errorMessage === undefined || errorMessage === STRING_EMPTY)
            setDatePickerStyle(INPUTS_DEFAULT_STYLE + INPUTS_NORMAL_STYLE)
        else 
            setDatePickerStyle(INPUTS_DEFAULT_STYLE + INPUTS_ERROR_STYLE)
    }, [errorMessage])

    return (
        <div>
            {/* Date Picker Label */}
            <label className='flex'>
                {required && <p className={INPUTS_REQUIRED_STYLE}>*</p>}
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
                <span className={INPUTS_ERROR_MESSAGE_STYLE}>
                    {errorMessage}
                </span>
            }
        </div>
    )
}

export default DatePicker