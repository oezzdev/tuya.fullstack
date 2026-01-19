using Cards.Domain.Entities;
using Cards.Domain.Ports;
using Shared.Application.Cqrs;
using Shared.Application.Results;

namespace Cards.Application.Commands.UpdateCard;

public class UpdateCardHandler(ICardRepository cardRepository) : IRequestHandler<UpdateCardCommand>
{
    public async Task<Result> Handle(UpdateCardCommand request, CancellationToken cancellationToken = default)
    {
        Card? card = await cardRepository.GetById(request.Id, cancellationToken);
        if (card is null)
        {
            return Error.NotFound("Card not found");
        }

        card.HolderName = request.HolderName;
        card.Status = request.Status;
        await cardRepository.Update(card, cancellationToken);
        return Result.Success();
    }
}