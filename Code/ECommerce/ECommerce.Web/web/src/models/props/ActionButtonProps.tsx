import { ReactNode } from "react";

export interface ActionButtonProps {
    icon?:ReactNode;
    type: "primary" | "secondary" | "success" | "warning" | "danger" | "info" | "dark" 
    label:string;
    onButtonClickedHandler: () => void;
}