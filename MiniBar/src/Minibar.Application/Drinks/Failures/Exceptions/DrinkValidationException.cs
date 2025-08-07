using Minibar.Application.Exceptions;
using Shared;

namespace Minibar.Application.Drinks.Failures.Exceptions
{
    public class DrinkValidationException : BadRequestException
    {
        public DrinkValidationException(Error[] errors)
            : base(errors)
        {
        }
    }
}
