using Shared;

namespace Minibar.Application.Categories.Failures
{
    public partial class Errors
    {
        public static class Categories
        {
            public static Error CategoryAlreadyExist() =>
                Error.Failure(
                        "category_already_exists",
                        "Категория с таким названием уже есть!");
        }
    }
}
