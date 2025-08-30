import React, {useState, useRef, type ChangeEvent, useEffect} from 'react';
import '../styles/App.css'
import '../styles/DrinkAddingForm.css';

interface DrinkData {
    name: string;
    description: string;
    photo: File | null;
    categoryId: number | ''; // может быть пустым
    userId: number;
    tagsIds: number[];
}

interface Category {
    id: number;
    name: string;
}

function AddDrinkPage() {

    const [drinkData, setDrinkData] = useState<DrinkData>({
        name: '',
        description: '',
        photo: null,
        categoryId: '', // пустое изначально
        userId: 1,
        tagsIds: []
    });

    const [categories, setCategories] = useState<Category[]>([]);
    const [preview, setPreview] = useState<string | null>(null);
    const [formErrors, setFormErrors] = useState<{ categoryId?: string }>({});
    const fileInputRef = useRef<HTMLInputElement>(null);
    const [submitting, setSubmitting] = useState(false);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchCategories = async() =>{
            try {
                setLoading(true);
                const responce = await fetch("/api/Categories/GetAll");

                if(!responce.ok) {
                    throw new Error("Нет ответа от базы с категориями");
                }

                const data: Category[] = await responce.json();
                setCategories(data);

                // Устанавливаем первую группу по умолчанию, если группы загружены
                if (data.length > 0) {
                    setDrinkData(prev => ({ ...prev, categoryId: data[0].id }));
                }
            } catch (err) {
                setError(err instanceof Error ? err.message : "Непонятно что произошло в БД с категориями");
            } finally {
                setLoading(false);
            }
        };
        fetchCategories();
    }, []);

    const handleInputChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setDrinkData(prev => ({
            ...prev,
            [name]: name === 'categoryId' ? (value === '' ? '' : parseInt(value, 10)) : value
        }));

        // Очищаем ошибку при изменении выбора
        if (name === 'categoryId' && formErrors.categoryId) {
            setFormErrors(prev => ({ ...prev, categoryId: undefined }));
        }
    };

    const validateForm = (): boolean => {
        const errors: { [key: string]: string } = {};

        if (!drinkData.name.trim()) {
            errors.name = 'Название обязательно!';
        }

        if (drinkData.categoryId === '') {
            errors.categoryId = 'Пожалуйста, выберите группу';
        }

        setFormErrors(errors);
        return Object.keys(errors).length === 0;
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

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!validateForm()) {
            // Прокрутка к ошибке
            const errorElement = document.querySelector('.error-message');
            errorElement?.scrollIntoView({ behavior: 'smooth', block: 'center' });
            return;
        }

        setSubmitting(true);
        setError(null);

        try {
            // Создаем FormData для отправки файла
            const formData = new FormData();
            formData.append('name', drinkData.name);
            formData.append('description', drinkData.description);
            formData.append('categoryId', drinkData.categoryId.toString());
            /*if (drinkData.photo) {
                formData.append('photo', drinkData.avatar); // пока без отправки фото, надо переделать сервис
            }*/
            formData.append('picturePath', '');
            formData.append('userId', drinkData.userId.toString());
            drinkData.tagsIds.forEach((num, index) => {
                formData.append(`tagsIds[${index}]`, num.toString());
            });

            const response = await fetch("/api/Drinks/Create", {
                method: 'POST',
                body: formData,
                // headers НЕ нужны для FormData - браузер сам установит multipart/form-data
            });

            if (!response.ok) {
                const errorData = await response.json();

                // Если это массив, берем первый элемент
                if (Array.isArray(errorData) && errorData.length > 0) {
                    const firstError = errorData[0];
                    throw new Error(firstError.message || firstError.Message || `Что-то пошло не так :с`);
                } else {
                    // Если это объект, работаем как обычно
                    throw new Error(errorData.message || errorData.Message || `Что-то пошло не так :с`);
                }
            }

            // Сброс формы
            setDrinkData({
                name: '',
                description: '',
                photo: null,
                categoryId: '',
                userId: 1,
                tagsIds: []
            });

            setPreview(null);
            setFormErrors({});

            alert('Напиток добавлен!');

        } catch (err) {
            setError(err instanceof Error ? err.message : 'Неизвестная ошибка при отправке');
        } finally {
            setSubmitting(false);
        }

        // Здесь будет логика отправки данных
        console.log('Данные о напитке:', drinkData);


    };

    return (
        <div className="app">
            <main className="main">
                <h1>Добавление напитка</h1>
                {loading && <div className="loading">Загрузка категорий...</div>}
                {error && <div className="error">Ошибка: {error}</div>}
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
                            <label htmlFor="categoryId">Категория *</label>
                            <select
                                id="categoryId"
                                name="categoryId"
                                value={drinkData.categoryId}
                                onChange={handleInputChange}
                                required
                                disabled={loading}
                                className={formErrors.categoryId ? 'error' : ''}
                            >
                                <option value="">-- Выберите категорию --</option>
                                {categories.map(category => (
                                    <option key={category.id} value={category.id}>
                                        {category.name}
                                    </option>
                                ))}
                            </select>

                            {formErrors.categoryId && (
                                <div className="error-message">{formErrors.categoryId}</div>
                            )}

                            {categories.length === 0 && !loading && (
                                <div className="warning">Группы не загружены</div>
                            )}
                        </div>

                        <button
                            type="submit"
                            className="submit-btn"
                            disabled={loading || categories.length === 0}
                        >
                            {submitting ? 'Отправка...' : 'Добавить Напиток'}
                        </button>
                    </form>
                </div>
            </main>
        </div>
    )
}

export default AddDrinkPage;