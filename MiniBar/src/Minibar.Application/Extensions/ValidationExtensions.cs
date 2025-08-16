using FluentValidation.Results;
using Shared;

namespace Minibar.Application.Extensions
{
    public static class ValidationExtensions
    {
        public static Failure ToErrors(this ValidationResult validationResult)
        {
            return validationResult.Errors.Select(e => Error.NotValid(
                    e.ErrorCode, e.ErrorMessage, e.PropertyName)).ToArray();
        }
    }
}
