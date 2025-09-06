using Shared;

namespace Minibar.Application.Drinks.Failures
{
    public partial class Errors
    {
        public static class Drinks
        {
            public static Error DrinkAlreadyExist() =>
                Error.Failure(
                        "drink_already_exists",
                        "Напиток с таким названием уже есть!");

            public static Error IncorrectPhotoSize() =>
                Error.NotValid(
                    "incorrect_photo_size",
                    "Слишком большой развер фото! (> 5 Мб");

            public static Error IncorrectPhotoExtension() =>
                Error.NotValid(
                    "incorrect_photo_extension",
                    "Формат файла явно не для фото!");
        }
    }
}
