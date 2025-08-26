import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage.tsx";
import DrinksPage from "./pages/DrinksPage.tsx";
import AddDrinkPage from "./pages/AddDrinkPage.tsx";
import CategoriesPage from "./pages/CategoriesPage.tsx";
import CocktailsPage from "./pages/CocktailsPage.tsx";
import IngredientsPage from "./pages/IngredientsPage.tsx";
import Header from "./components/Header.tsx";

function App() {
  return (
      <Router>
          <Header />
          <Routes>
              <Route path="/" element={<HomePage />} />
              <Route path="/drinks" element={<DrinksPage />} />
              <Route path="/drinks/add" element={<AddDrinkPage />} />
              <Route path="/categories" element={<CategoriesPage />} />
              <Route path="/cocktails" element={<CocktailsPage />} />
              <Route path="/ingredients" element={<IngredientsPage />} />
          </Routes>
      </Router>
  )
}

export default App
