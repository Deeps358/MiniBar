namespace Shared
{
    public record Error
    {
        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }
        public string? InvalidField { get; }

        private Error(string code,
                      string message,
                      ErrorType type,
                      string? invalidField = null)
        {
            Code = code;
            Message = message;
            Type = type;
            InvalidField = invalidField;
        }

        public static Error NotValid(string? code, string message, string? invalidField = null)
            => new(code ?? "Value_Is_Invalid", message, ErrorType.VALIDATION, invalidField);
        public static Error NotFound(string? code, string message, Guid? id)
            => new(code ?? "Record_Not_Found", message, ErrorType.NOT_FOUND);
        public static Error Failure(string? code, string message)
            => new(code ?? "Failure", message, ErrorType.FAILURE);
        public static Error Conflict(string? code, string message)
            => new(code ?? "Value_Is_Conflict", message, ErrorType.CONFLICT);

    }

    public enum ErrorType
    {
        VALIDATION, // ввели что-то не то
        NOT_FOUND, // не найдено
        FAILURE, // ошибка на сервере
        CONFLICT, // например запись в БД уже есть
    }
}
