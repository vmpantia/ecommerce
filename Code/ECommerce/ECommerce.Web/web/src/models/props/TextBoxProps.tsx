export interface TextBoxProps {
    type: "text" | "password" | "email" | undefined;
    placeholder:string;
    required?:boolean;
    name:string;
    label:string;
    value:string | number | readonly string[] | undefined;
    errorMessage?:string;
    isDisabled:boolean;
    onValueChangedHandler: (e:any) => void;    
}