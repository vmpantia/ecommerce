export interface DatePickerProps {
    type: "date" | undefined;
    required?:boolean;
    name:string;
    label:string;
    value:string | number | readonly string[] | undefined;
    errorMessage?:string;
    isDisabled:boolean;
    onValueChangedHandler: (e:any) => void;    
}