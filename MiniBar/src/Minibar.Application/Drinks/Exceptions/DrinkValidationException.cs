using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Minibar.Application.Exceptions;
using Shared;

namespace Minibar.Application.Drinks.Exceptions
{
    public class DrinkValidationException : BadRequestException
    {
        public DrinkValidationException(Error[] errors)
            : base(errors)
        {
        }
    }
}
