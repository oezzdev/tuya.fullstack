using Shared.Application.Results;

namespace Payments.Application.Ports;

public interface ICardsService
{
    Task<Result> Discount(Guid cardId, decimal amount, CancellationToken cancellationToken = default);
}
