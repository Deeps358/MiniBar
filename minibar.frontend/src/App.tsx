import './App.css'
import { FaWineBottle, FaThList, FaCocktail, FaLemon } from "react-icons/fa";

function App() {
  const currentUser = "Deeps";

  return (
    <div className="app">
        <header className="header">
            <div className="logo">Минибар</div>
            <nav className="nav">
                <a href="#"><FaWineBottle />Напитки</a>
                <a href="#"><FaThList />Категории</a>
                <a href="#"><FaCocktail />Коктейли</a>
                <a href="#"><FaLemon />Ингридиенты</a>
            </nav>
            <div className="user">{currentUser}</div>
            <div className="header-decoration"></div>
        </header>
        <main className="main">
            <h1>Главная страница, пока не придумал что тут будет, выбери любую другую.</h1>
        </main>
    </div>
  )
}

export default App
