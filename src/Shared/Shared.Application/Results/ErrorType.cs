using System.Text.Json.Serialization;

namespace Shared.Application.Results;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ErrorType
{
    NotFound,
    Validation,
    Conflict,
    Unauthorized,
    Forbidden,
    Unexpected
}