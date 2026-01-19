using Cards.Application.Commands.Discount;
using Payments.Application.Ports;
using Shared.Application.Cqrs;
using Shared.Application.Results;

namespace Payments.Infrastructure.Services;

public class CardsService(IMediator mediator) : ICardsService
{
    public Task<Result> Discount(Guid cardId, decimal amount, CancellationToken cancellationToken = default)
    {
        return mediator.Send<DiscountCommand>(new(cardId, amount), cancellationToken);
    }
}
