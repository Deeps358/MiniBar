import  React from "react";
import { useNavigate } from 'react-router-dom';
import { FaPlus } from "react-icons/fa";
import "../styles/AddButton.css";

interface AddButtonProps {
    text?: string;
    linkTo?: string;
}

const AddButton : React.FC<AddButtonProps> = ({ text, linkTo }) =>  {
    const navigate = useNavigate();

    const handleClick = () => {
        if (linkTo) {
            navigate(linkTo);
        }
    }

    return (
        <button className="add-button" onClick={handleClick}>
            {text && <FaPlus />}
            {text || 'Добавить'}
        </button>
    )
}

export default AddButton;