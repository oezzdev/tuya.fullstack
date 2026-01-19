namespace Shared.Application.Results;

public record Error(string Id, ErrorType Type, string Message, IEnumerable<string>? Details = default)
{
    public static Error NotFound(string message) => new("NotFound", ErrorType.NotFound, message);
    public static Error Validation(IEnumerable<string>? details) => new("ValidationFailed", ErrorType.Validation, "Errores de validación.", details);
    public static Error Validation(string message) => new("ValidationFailed", ErrorType.Validation, message);
    public static Error Conflict(string message) => new("Conflict", ErrorType.Conflict, message);
    public static Error Unauthorized(string message) => new("Unauthorized", ErrorType.Unauthorized, message);
    public static Error Forbidden(string message) => new("Forbidden", ErrorType.Forbidden, message);
    public static Error Unexpected(string message) => new("Unexpected", ErrorType.Unexpected, message);
}