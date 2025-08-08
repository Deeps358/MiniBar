import './App.css'
import { FaWineBottle, FaThList, FaCocktail, FaLemon, FaUser } from "react-icons/fa";

function App() {
  const currentUser = "Deeps";

  return (
    <div className="app">
        <header className="header">
            {/* Название бара */}
            <div className="bar-name">Минибар</div>

            {/* Левая навигация */}
            <nav className="nav-left">
                <a href="#">
                    <FaWineBottle />Напитки
                </a>
                <a href="#">
                    <FaThList />Категории
                </a>
            </nav>
            {/* Логотип по центру */}
            <div className="logo-container">
                <img
                    src="/PapuBarmen2.jpg"
                    alt="Минибар"
                    className="logo"
                />
            </div>
            {/* Правая навигация */}
            <nav className="nav-right">
                <a href="#">
                    <FaCocktail />Коктейли
                </a>
                <a href="#">
                    <FaLemon />Ингридиенты
                </a>
            </nav>
            {/* Блок пользователя */}
            <div className="user-container">
                <div className="user">
                    <FaUser /> {currentUser}
                </div>
            </div>
            <div className="header-decoration"></div>
        </header>
        <main className="main">
            <h1>Главная страница, пока не придумал что тут будет, выбери любую другую.</h1>
        </main>
    </div>
  )
}

export default App
