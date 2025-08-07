using Minibar.Application.Exceptions;
using Shared;

namespace Minibar.Application.Users.Failures.Exceptions
{
    public class UserAlreadyExistsException : BadRequestException
    {
        public UserAlreadyExistsException(Error[] errors)
            : base(errors)
        {

        }
    }
}
