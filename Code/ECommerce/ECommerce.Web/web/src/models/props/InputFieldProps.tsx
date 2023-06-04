export interface InputFieldProps {
    type:React.HTMLInputTypeAttribute | undefined;
    placeholder?:string;
    required?:boolean;
    name:string;
    label:string;
    value:string | number | readonly string[] | undefined;
    onValueChangedHandler: (e:any) => void;    
}