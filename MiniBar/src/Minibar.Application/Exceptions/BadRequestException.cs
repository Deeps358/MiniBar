using Shared;
using System.Text.Json;

namespace Minibar.Application.Exceptions
{
    public class BadRequestException : Exception // 500
    {
        public BadRequestException(Error[] errors)
            : base(JsonSerializer.Serialize(errors))
        {

        }
    }
}
