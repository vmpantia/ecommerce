import { Link } from "react-router-dom"

//Properties
import { LinkButtonProps } from "../../models/props/LinkButtonProps"

const LinkButton = ({label, url}:LinkButtonProps) => {
  return (
    <Link to={url} className="text-sm mx-2 text-blue-600 hover:text-blue-700 hover:underline underline-offset-4">
        {label}
    </Link>
  )
}

export default LinkButton