import { InputFieldProps } from "../models/props/InputFieldProps"

const InputField = ({type, placeholder, required, name, label, value, onValueChangedHandler}:InputFieldProps) => {
    return (
        <div>
            <label className='text-sm flex'>
                {required && <p className="text-red-600 font-medium mr-1">*</p>}
                {label}:
            </label>
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