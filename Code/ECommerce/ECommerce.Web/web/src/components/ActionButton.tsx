//Properties
import { ActionButtonProps } from '../models/props/ActionButtonProps'

//Utilities
import { BUTTON_DANGER_STYLE, BUTTON_DARK_STYLE, BUTTON_DEFAULT_STYLE, BUTTON_INFO_STYLE, BUTTON_PRIMARY_STYLE, BUTTON_SECONDARY_STYLE, BUTTON_SUCCESS_STYLE, BUTTON_WARNING_STYLE } from '../utils/Constants';

const ActionButton = ({icon, type, label, isDisabled, onButtonClickedHandler}:ActionButtonProps) => {
  const getBtnStyle = () => {
    switch(type){
      case "primary":
        return BUTTON_DEFAULT_STYLE + BUTTON_PRIMARY_STYLE;
      case "secondary":
        return BUTTON_DEFAULT_STYLE + BUTTON_SECONDARY_STYLE;
      case "success":
        return BUTTON_DEFAULT_STYLE + BUTTON_SUCCESS_STYLE;
      case "warning":
        return BUTTON_DEFAULT_STYLE + BUTTON_WARNING_STYLE;
      case "danger":
        return BUTTON_DEFAULT_STYLE + BUTTON_DANGER_STYLE;
      case "info":
        return BUTTON_DEFAULT_STYLE + BUTTON_INFO_STYLE;
      case "dark":
        return BUTTON_DEFAULT_STYLE + BUTTON_DARK_STYLE;
      default:
        return BUTTON_DEFAULT_STYLE;
    }
  }

  return (
    <button className={getBtnStyle()} 
            disabled={isDisabled} 
            onClick={onButtonClickedHandler}>
      {icon &&
        <div className='w-3 mr-2 mt-0.5'>
        {icon}
        </div>
      }
      {label.toUpperCase()}
    </button>
  )
}

export default ActionButton