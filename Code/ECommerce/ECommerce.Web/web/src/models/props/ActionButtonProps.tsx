export interface ActionButtonProps {
    type: "primary" | "secondary" | "success" | "warning" | "danger" | "info" | "dark" 
    label:string;
    onButtonClickedHandler: () => void;
}