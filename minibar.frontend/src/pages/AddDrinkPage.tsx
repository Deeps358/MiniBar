import React, { useState, useRef, type ChangeEvent } from 'react';
import '../styles/App.css'
import '../styles/DrinkAddingForm.css';

interface DrinkData {
    name: string;
    description: string;
    photo: File | null;
    category: string;
}

function AddDrinkPage() {

    const [drinkData, setDrinkData] = useState<DrinkData>({
        name: '',
        description: '',
        photo: null,
        category: 'user'
    });

    const [preview, setPreview] = useState<string | null>(null);
    const fileInputRef = useRef<HTMLInputElement>(null);

    const handleInputChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setDrinkData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0] || null;

        if (file) {
            setDrinkData(prev => ({
                ...prev,
                photo: file
            }));

            // Создание превью для изображения
            const reader = new FileReader();
            reader.onloadend = () => {
                setPreview(reader.result as string);
            };
            reader.readAsDataURL(file);
        }
    };

    const handleRemoveImage = () => {
        setDrinkData(prev => ({
            ...prev,
            photo: null
        }));
        setPreview(null);
        if (fileInputRef.current) {
            fileInputRef.current.value = '';
        }
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        // Здесь будет логика отправки данных
        console.log('Данные о напитке:', drinkData);

        // Можно добавить отправку на сервер
        // const formData = new FormData();
        // formData.append('login', userData.login);
        // formData.append('description', userData.description);
        // formData.append('group', userData.group);
        // if (userData.avatar) {
        //   formData.append('avatar', userData.avatar);
        // }

        alert('Напиток добавлен!');

        // Сброс формы
        setDrinkData({
            name: '',
            description: '',
            photo: null,
            category: 'user'
        });
        setPreview(null);
    };

    return (
        <div className="app">
            <main className="main">
                <h1>Добавление напитка</h1>
                <div className="drink-form-container">
                    <form onSubmit={handleSubmit} className="drink-form">
                        <div className="form-group">
                            <label htmlFor="name">Название *</label>
                            <input
                                type="text"
                                id="name"
                                name="name"
                                value={drinkData.name}
                                onChange={handleInputChange}
                                required
                                placeholder="Введите название напитка"
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="description">Описание</label>
                            <textarea
                                id="description"
                                name="description"
                                value={drinkData.description}
                                onChange={handleInputChange}
                                rows={4}
                                placeholder="Описание напитка..."
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="photo">Фото</label>
                            <div className="file-upload">
                                <input
                                    ref={fileInputRef}
                                    type="file"
                                    id="photo"
                                    name="photo"
                                    onChange={handleFileChange}
                                    accept="image/*"
                                    className="file-input"
                                />
                                <label htmlFor="photo" className="file-label">
                                    Выберите файл
                                </label>
                                {preview && (
                                    <button
                                        type="button"
                                        onClick={handleRemoveImage}
                                        className="remove-image-btn"
                                    >
                                        Удалить
                                    </button>
                                )}
                            </div>

                            {preview && (
                                <div className="image-preview">
                                    <img src={preview} alt="Предпросмотр фото" />
                                </div>
                            )}
                        </div>

                        <div className="form-group">
                            <label htmlFor="group">Категория *</label>
                            <select
                                id="group"
                                name="group"
                                value={drinkData.category}
                                onChange={handleInputChange}
                                required
                            >
                                <option value="admin">Администратор</option>
                                <option value="manager">Менеджер</option>
                                <option value="user">Пользователь</option>
                            </select>
                        </div>

                        <button type="submit" className="submit-btn">
                            Добавить напиток
                        </button>
                    </form>
                </div>
            </main>
        </div>
    )
}

export default AddDrinkPage;