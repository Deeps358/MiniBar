using Shared;
using System.Text.Json;

namespace Minibar.Application.Exceptions
{
    public class NotFoundException : Exception // 404
    {
        public NotFoundException(IEnumerable<Error> errors)
            : base(JsonSerializer.Serialize(errors))
        {

        }
    }
}
