using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Minibar.Controllers.ResponceExtensions
{
    public static class ResponceExtensions
    {
        public static ActionResult ToResponce(this Failure failure)
        {
            if(!failure.Any())
            {
                return new ObjectResult(null) // вдруг ошибок нет
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            // теперь надо перебрать ошибки и посчитать каких кодов больше

            var distinctErrorTypes = failure
                .Select (t => t.Type) // сначала собираем только типы ошибок
                .Distinct() // убираем дубли
                .ToList(); // в список

            int statusCode = distinctErrorTypes.Count > 1
                ? StatusCodes.Status500InternalServerError // если их там прям много, то лучше 500 кинуть
                : GetStatusCodeFromErrorType(distinctErrorTypes.First());

            return new ObjectResult(failure)
            {
                StatusCode = statusCode,
            };
        }

        private static int GetStatusCodeFromErrorType(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.VALIDATION => StatusCodes.Status400BadRequest,
                ErrorType.NOT_FOUND => StatusCodes.Status404NotFound,
                ErrorType.CONFLICT => StatusCodes.Status409Conflict,
                ErrorType.FAILURE => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError,
            };
    }
}
