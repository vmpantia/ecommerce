//Properties
import { ActionButtonProps } from '../models/props/ActionButtonProps'

const ActionButton = ({icon, type, label, onButtonClickedHandler}:ActionButtonProps) => {
  
  //defaultBtnStyle: Use to set default style or className of button
  let defaultBtnStyle = "inline-flex py-1.5 px-5 mr-2 text-sm rounded "

  const getBtnStyle = () => {
    switch(type){
      case "primary":
        return defaultBtnStyle + "bg-blue-600 text-white hover:bg-blue-700";
      case "secondary":
        return defaultBtnStyle + "bg-gray-600 text-white hover:bg-gray-700";
      case "success":
        return defaultBtnStyle + "bg-green-600 text-white hover:bg-green-700";
      case "warning":
        return defaultBtnStyle + "bg-orange-500 text-white hover:bg-orange-600";
      case "danger":
        return defaultBtnStyle + "bg-red-600 text-white hover:bg-red-700";
      case "info":
        return defaultBtnStyle + "bg-zinc-200 hover:bg-zinc-300";
      case "dark":
        return defaultBtnStyle + "bg-neutral-800 text-white hover:bg-neutral-900";
      default:
        return defaultBtnStyle;
    }
  }

  return (
    <button className={getBtnStyle()} onClick={onButtonClickedHandler}>
      {icon &&
        <div className='w-4 mr-2 mt-0.5'>
        {icon}
        </div>
      }
      {label}
    </button>
  )
}

export default ActionButton