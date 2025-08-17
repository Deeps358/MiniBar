import '../styles/App.css'
import '../styles/CategoriesList.css'
import {useEffect, useState} from "react";

interface Category {
    id: number;
    name: string;
}

function CategoriesPage() {

    const [categories, setCategories] = useState<Category[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchCategories = async() =>{
            try {
                const responce = await fetch("/api/Categories/GetAll");

                if(!responce.ok) {
                    throw new Error("Нет ответа от базы с категориями");
                }

                const data: Category[] = await responce.json();
                setCategories(data);
            } catch (err) {
                setError(err instanceof Error ? err.message : "Непонятно что произошло в БД с категориями");
            } finally {
                setLoading(false);
            }
        };
        fetchCategories();
    }, []);

    return (
        <div className="app">
            <main className="main">
                <h1>Категории напитков</h1>
                {loading? (<div>Загрузка...</div>) : null}
                {error? (<div>Ошибка: {error}</div>) : null}
                <div className="inline-container">
                    <div className="inline-list">
                        {categories.map(category => (
                            <span key={category.id} className="inline-item">{category.name}</span>
                        ))}
                    </div>
                </div>
            </main>
        </div>
    )
}

export default CategoriesPage;