using Minibar.Application.Exceptions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibar.Application.Drinks.Exceptions
{
    public class DrinkNotFoundException : NotFoundException
    {
        public DrinkNotFoundException(Error[] errors)
            : base(errors)
        {
        }
    }
}
