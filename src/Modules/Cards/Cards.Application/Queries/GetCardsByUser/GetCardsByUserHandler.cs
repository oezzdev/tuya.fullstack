using Cards.Domain.Ports;
using Shared.Application.Cqrs;
using Shared.Application.Results;

namespace Cards.Application.Queries.GetCardsByUser;

public class GetCardsByUserHandler(ICardRepository cardRepository) : IRequestHandler<GetCardsByUserQuery, IEnumerable<GetCardsByUserResult>>
{
    public async Task<Result<IEnumerable<GetCardsByUserResult>>> Handle(GetCardsByUserQuery request, CancellationToken cancellationToken = default)
    {
        var cards = await cardRepository.GetByUser(request.CustomerId, cancellationToken);
        return cards.Select(card => new GetCardsByUserResult(
            card.Id,
            card.UserId,
            card.Number,
            card.HolderName,
            card.ExpirationDate,
            card.Cvv,
            card.Balance,
            card.Status
        )).ToList();
    }
}