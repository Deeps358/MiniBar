using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Minibar.Application.Exceptions
{
    public class NotFoundException : Exception // 404
    {
        public NotFoundException(Error[] errors)
            : base(JsonSerializer.Serialize(errors))
        {

        }
    }
}
