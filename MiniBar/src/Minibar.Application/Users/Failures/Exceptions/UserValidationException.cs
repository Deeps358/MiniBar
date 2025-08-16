using Minibar.Application.Exceptions;
using Shared;

namespace Minibar.Application.Users.Failures.Exceptions
{
    public class UserValidationException : BadRequestException
    {
        public UserValidationException(Failure errors)
            : base(errors)
        {

        }
    }
}
