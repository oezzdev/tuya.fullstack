using Cards.Domain.Entities;
using Cards.Domain.Ports;
using Shared.Application.Cqrs;
using Shared.Application.Results;

namespace Cards.Application.Commands.CreateCard;

public class CreateCardHandler(ICardRepository cardRepository) : IRequestHandler<CreateCardCommand, CreatedCardResult>
{
    public async Task<Result<CreatedCardResult>> Handle(CreateCardCommand request, CancellationToken cancellationToken = default)
    {
        Card card = new(request.UserId, request.Number, request.HolderName, request.ExpirationDate, request.Cvv, request.Balance);
        await cardRepository.Add(card, cancellationToken);

        return new CreatedCardResult(card.Id, card.UserId, card.Number, card.HolderName, card.ExpirationDate, card.Cvv, card.Balance);
    }
}