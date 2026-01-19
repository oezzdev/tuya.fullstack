using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Cqrs;
using Shared.Application.Results;

namespace Shared.Api.Cqrs;

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public async Task<Result> Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        IRequestHandler<TRequest> handler = serviceProvider.GetRequiredService<IRequestHandler<TRequest>>();
        Result result = await ValidateAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            return result;
        }
        return await handler.Handle(request, cancellationToken);
    }

    public async Task<Result<TResponse>> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
    {
        IRequestHandler<TRequest, TResponse> handler = serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        Result result = await ValidateAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            return result.Error;
        }
        return await handler.Handle(request, cancellationToken);
    }

    /// <summary>
    /// Asynchronously validates the specified request using all registered validators for the request type.
    /// </summary>
    /// <remarks>If no validators are registered for the request type, the method returns a success result.
    /// All registered validators are executed, and their errors are aggregated.</remarks>
    /// <typeparam name="TRequest">The type of the request to validate.</typeparam>
    /// <param name="request">The request object to be validated. Cannot be null.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the validation operation.</param>
    /// <returns>A result indicating whether the request passed validation. Returns a failure result containing validation errors
    /// if any validators report failures; otherwise, returns a success result.</returns>
    private async Task<Result> ValidateAsync<TRequest>(TRequest request, CancellationToken cancellationToken)
    {
        var validators = serviceProvider.GetServices<IValidator<TRequest>>();

        if (!validators.Any())
        {
            return Result.Success();
        }

        ValidationContext<TRequest> context = new(request);
        ValidationResult[] validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        List<ValidationFailure> failures = [.. validationResults.SelectMany(r => r.Errors).Where(f => f != null)];

        if (failures.Count == 0)
        {
            return Result.Success();
        }

        Error error = Error.Validation(failures.Select(x => x.ErrorMessage));
        return Result.Failure(error);
    }
}
