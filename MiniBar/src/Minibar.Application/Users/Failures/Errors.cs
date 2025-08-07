using Shared;

namespace Minibar.Application.Users.Failures
{
    public partial class Errors
    {
        public static class Users
        {
            public static Error UserAlreadyExist() =>
                Error.Failure(
                        "user_already_exists",
                        "Пользователь с таким мылом уже есть!");
        }
    }
}
