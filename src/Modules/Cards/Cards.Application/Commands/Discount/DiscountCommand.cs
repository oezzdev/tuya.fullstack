using Cards.Domain.Entities;
using Cards.Domain.Ports;
using Shared.Application.Cqrs;
using Shared.Application.Results;

namespace Cards.Application.Commands.Discount;

public record DiscountCommand(Guid CardId, decimal Amount) : IRequest;

public class DiscountCommandHandler(ICardRepository cardRepository) : IRequestHandler<DiscountCommand>
{
    public async Task<Result> Handle(DiscountCommand request, CancellationToken cancellationToken)
    {
        Card? card = await cardRepository.GetById(request.CardId, cancellationToken);
        if (card is null)
        {
            return Error.NotFound("Card not found.");
        }

        if (card.Balance < request.Amount)
        {
            return Error.Validation("Insufficient balance on the card.");
        }

        card.Balance -= request.Amount;
        await cardRepository.Update(card, cancellationToken);

        return Result.Success();
    }
}
