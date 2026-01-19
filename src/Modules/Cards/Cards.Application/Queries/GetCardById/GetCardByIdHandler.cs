using Cards.Domain.Entities;
using Cards.Domain.Ports;
using Shared.Application.Cqrs;
using Shared.Application.Results;

namespace Cards.Application.Queries.GetCardById;

public class GetCardByIdHandler(ICardRepository cardRepository) : IRequestHandler<GetCardByIdQuery, GetCardByIdResult>
{
    public async Task<Result<GetCardByIdResult>> Handle(GetCardByIdQuery request, CancellationToken cancellationToken = default)
    {
        Card? card = await cardRepository.GetById(request.Id, cancellationToken);
        if (card is null)
        {
            return Error.NotFound("Card not found");
        }

        return new GetCardByIdResult(card.Id, card.UserId, card.Number, card.HolderName, card.ExpirationDate, card.Cvv, card.Balance);
    }
}