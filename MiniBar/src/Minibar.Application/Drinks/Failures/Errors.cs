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
        }
    }
}
