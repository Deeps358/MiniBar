using Minibar.Application.Exceptions;
using Shared;

namespace Minibar.Application.Users.Failures.Exceptions
{
    public class UserValidationException : BadRequestException
    {
        public UserValidationException(Error[] errors)
            : base(errors)
        {

        }
    }
}
