using Shared.Application.Results;

namespace Shared.Application.Cqrs;

public interface IMediator
{
    /// <summary>
    /// Sends a request asynchronously and returns the result of the operation.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request to send. Must implement the IRequest interface.</typeparam>
    /// <param name="request">The request object to be sent. Cannot be null.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous send operation. The task result contains a Result object indicating the
    /// outcome of the request.</returns>
    Task<Result> Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest;
    /// <summary>
    /// Sends a request and asynchronously returns the result produced by the corresponding handler.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request to send. Must implement <see cref="IRequest{TResponse}"/>.</typeparam>
    /// <typeparam name="TResponse">The type of the response expected from the request.</typeparam>
    /// <param name="request">The request object to be sent. Must not be null.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{TResponse}"/>
    /// representing the outcome of the request.</returns>
    Task<Result<TResponse>> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>;
}
