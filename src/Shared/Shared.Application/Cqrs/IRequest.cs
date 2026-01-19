namespace Shared.Application.Cqrs;

public interface IRequest;

public interface IRequest<out TResponse>;
