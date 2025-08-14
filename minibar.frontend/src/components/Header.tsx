import '../styles/App.css'
import { FaWineBottle, FaThList, FaCocktail, FaLemon, FaUser } from "react-icons/fa";
import { Link } from "react-router-dom";

function Header() {
    const currentUser = "Deeps";

    return (
        <header className="header">

            {/* Название бара */}
            <div className="bar-name">
                <Link to="/">
                    Минибар
                </Link>
            </div>

            {/* Левая навигация */}
            <nav className="nav-left">
                <Link to= "/drinks">
                    <FaWineBottle />Напитки
                </Link>
                <Link to="/categories">
                    <FaThList />Категории
                </Link>
            </nav>

            {/* Логотип по центру */}
            <div className="logo-container">
                <img
                    src="/header_image.jpg"
                    alt="Минибар"
                    className="logo"
                />
            </div>

            {/* Правая навигация */}
            <nav className="nav-right">
                <Link to="/cocktails">
                    <FaCocktail />Коктейли
                </Link>
                <Link to="/ingredients">
                    <FaLemon />Ингридиенты
                </Link>
            </nav>

            {/* Блок пользователя */}
            <div className="user-container">
                <div className="user">
                    <FaUser /> {currentUser}
                </div>
            </div>

            <div className="header-decoration"></div>
        </header>
    )
}

export default Header;