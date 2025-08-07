using Minibar.Application.Exceptions;
using Shared;

namespace Minibar.Application.Drinks.Failures.Exceptions
{
    public class DrinkNotFoundException : NotFoundException
    {
        public DrinkNotFoundException(Error[] errors)
            : base(errors)
        {
        }
    }
}
