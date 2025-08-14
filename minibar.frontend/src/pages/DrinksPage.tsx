import "../styles/App.css";
import "../styles/DrinksTable.css";
import {useEffect, useState} from "react";

interface Drink {
    id: number;
    name: string;
    description: string;
    picturePath: string;
    userId: number;
    categoryId: number;
    tags: number[];
    createdAt: Date;
}

interface Category {
    id: number;
    name: string;
}

function DrinksPage() {
    const [drinks, setDrinks] = useState<Drink[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchDrinks = async() =>{
            try {
                const responce = await fetch("/api/Drinks/GetAll");

                if(!responce.ok) {
                    throw new Error("Нет ответа от базы с напитками");
                }

                const data: Drink[] = await responce.json();
                setDrinks(data);
            } catch (err) {
                setError(err instanceof Error ? err.message : "Непонятно что произошло в БД с напитками");
                /*console.error("Failed to fetch drinks: ", err);*/
            } finally {
                setLoading(false);
            }
        };
        fetchDrinks();
    }, []);

    useEffect(() => {
        async function fetchCategories() {
            try {
                const categoriesIds = [...new Set(drinks.map(d => d.categoryId))];

                if (categoriesIds.length === 0) {
                    setCategories([]);
                    return;
                }

                const query = categoriesIds.map(id => `ids=${id}`).join('&');
                const responce = await fetch(`/api/Categories/GetFew?${query}`);

                if (!responce.ok) {
                    throw new Error("Не удалось получить информацию о категориях");
                }

                const categoriesData = await responce.json();
                setCategories(categoriesData);
            } catch (err) {
                setError(err instanceof Error ? err.message : 'Непонятно что при получении инфы о категориях');
                /*console.error('Failed to fetch groups:', err);*/
            } finally {
                setLoading(false);
            }
        }
        fetchCategories();
    }, [drinks]);

    return (
        <div className="app">
            <main className="main">
                <h1>Все напитки из базы</h1>
                {loading? (<div>Загрузка...</div>) : null}
                {error? (<div>Ошибка: {error}</div>) : null}
                <div className="drink-grid">
                    {drinks.map((drink, index) => (
                        <div key={index} className="drink-card">
                            <img
                                src={drink.picturePath || '/not_found_image.jpg'}
                                alt={drink.name}
                                className="drink-image"
                                onError={(e) => {
                                    const img = e.currentTarget; // Доступ к <img>, на котором произошла ошибка
                                    img.onerror = null; // Отключаем обработчик, чтобы избежать бесконечного цикла
                                    img.src = '/not_found_image.jpg'; // Устанавливаем fallback-изображение
                                }}
                            />
                            <a href={`/drinks/${drink.name}`} className="drink-name">{drink.name}</a>
                            <div className="drink-group">{
                                categories.find((category) => category.id === drink.categoryId)?.name
                                || "не удалось найти"}
                            </div>
                        </div>
                    ))}
                </div>
            </main>
        </div>
    )
}

export default DrinksPage;