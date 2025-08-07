using Minibar.Application.Exceptions;
using Shared;

namespace Minibar.Application.Drinks.Failures.Exceptions
{
    public class DrinkAlreadyExistException : BadRequestException
    {
        public DrinkAlreadyExistException(Error[] errors)
            : base(errors)
        {
        }
    }
}
