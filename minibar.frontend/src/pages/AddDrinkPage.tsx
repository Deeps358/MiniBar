import React, {useState, useRef, type ChangeEvent, useEffect} from 'react';
import '../styles/App.css'
import '../styles/DrinkAddingForm.css';

interface DrinkData {
    name: string;
    description: string;
    photo: File | null;
    categoryId: number | ''; // может быть пустым
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
        categoryId: '' // пустое изначально
    });

    const [categories, setCategories] = useState<Category[]>([]);
    const [preview, setPreview] = useState<string | null>(null);
    const [formErrors, setFormErrors] = useState<{ categoryId?: string }>({});
    const fileInputRef = useRef<HTMLInputElement>(null);
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
                    setDrinkData(prev => ({ ...prev, groupId: data[0].id }));
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
        const errors: { categoryId?: string } = {};

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

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        if (!validateForm()) {
            // Прокрутка к ошибке
            const errorElement = document.querySelector('.error-message');
            errorElement?.scrollIntoView({ behavior: 'smooth', block: 'center' });
            return;
        }

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
            categoryId: ''
        });
        setPreview(null);
        setFormErrors({});
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
                            Добавить напиток
                        </button>
                    </form>
                </div>
            </main>
        </div>
    )
}

export default AddDrinkPage;