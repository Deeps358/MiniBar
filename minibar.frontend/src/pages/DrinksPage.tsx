import "../App.css";
import {useEffect, useState} from "react";

interface Drink {
    id: number;
    name: string;
    description: string;
    userId: number;
    categoryId: number;
    tags: number[];
    createdAt: Date;
}

function DrinksPage() {
    const [drinks, setDrinks] = useState<Drink[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchDrinks = async() =>{
            try {
                const responce = await fetch("/api/Drinks/GetAll");

                if(!responce.ok) {
                    throw new Error("Failed to fetch drinks");
                }

                const data: Drink[] = await responce.json();
                setDrinks(data);
            } catch (err) {
                setError(err instanceof Error ? err.message : "Непонятно что произошло");
                console.error("Failed to fetch drinks: ", err);
            } finally {
                setLoading(false);
            }
        };

        fetchDrinks();
    }, []);

    return (
        <div className="app">
            <main className="main">
                <h1>Все напитки из базы</h1>
                {loading? (<div>Загрузка...</div>) : null}
                {error? (<div>Ошибка: {error}</div>) : null}
                <table>
                    <thead>
                        <tr>
                            <th>Название</th>
                            <th>Описание</th>
                        </tr>
                    </thead>
                    <tbody>
                    {drinks.map((drink, index) => (
                        <tr key={index}>
                            <td>{drink.name}</td>
                            <td>{drink.description}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </main>
        </div>
    )
}

export default DrinksPage;