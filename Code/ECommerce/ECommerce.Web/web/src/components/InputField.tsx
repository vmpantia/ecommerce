import { InputFieldProps } from "../models/props/InputFieldProps"

const InputField = ({type, placeholder, name, label, value, onValueChangedHandler}:InputFieldProps) => {
    return (
        <div>
            <label className='text-sm font-medium'>{label}:</label>
            <input className='w-full px-2 py-1.5 my-2 border rounded' 
                    type={type} 
                    placeholder={placeholder}
                    name={name}
                    value={value}
                    onChange={onValueChangedHandler} />
        </div>
    )
}

export default InputField