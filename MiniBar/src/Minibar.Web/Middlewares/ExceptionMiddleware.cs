using Minibar.Application.Exceptions;
using Shared;
using System.Text.Json;

namespace Minibar.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync (HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        // ответ пользователю если поймали ошибку, ещё и со статус кодом
        private async Task HandleExceptionAsync(HttpContext context, Exception exception) 
        {
            _logger.LogError(exception, exception.Message);

            (int code, Error[] errors) = exception switch
            {
                BadRequestException => (
                    StatusCodes.Status500InternalServerError, JsonSerializer.Deserialize<Error[]>(exception.Message)),

                NotFoundException => (
                    StatusCodes.Status404NotFound, JsonSerializer.Deserialize<Error[]>(exception.Message)),

                _ => (
                    StatusCodes.Status500InternalServerError, [Error.Failure(null, "Чёт пошло не так(")])
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;

            await context.Response.WriteAsJsonAsync(errors);
        }
    }

    public static class ExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(this WebApplication app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
