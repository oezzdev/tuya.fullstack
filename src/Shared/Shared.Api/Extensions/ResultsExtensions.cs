using Microsoft.AspNetCore.Mvc;
using Shared.Application.Results;

namespace Shared.Api.Extensions;

public static class ResultsExtensions
{
    public static ActionResult HandleError(this Error error) => error.Type switch
    {
        ErrorType.NotFound => new NotFoundObjectResult(error),
        ErrorType.Validation => new BadRequestObjectResult(error),
        ErrorType.Unauthorized => new UnauthorizedObjectResult(error),
        ErrorType.Forbidden => new ObjectResult(error) { StatusCode = 403 },
        ErrorType.Conflict => new ObjectResult(error) { StatusCode = 409 },
        ErrorType.Unexpected or _ => new ObjectResult(error) { StatusCode = 500 },
    };

    public static T Match<T>(this Result result, Func<T> onSuccess, Func<Error, T> onFailure)
        => result.IsSuccess ? onSuccess() : onFailure(result.Error);

    public static T Match<T, TValue>(this Result<TValue> result, Func<TValue, T> onSuccess, Func<Error, T> onFailure)
        => result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
}
