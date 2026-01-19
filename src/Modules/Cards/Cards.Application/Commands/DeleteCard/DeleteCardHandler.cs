using Cards.Domain.Entities;
using Cards.Domain.Ports;
using Shared.Application.Cqrs;
using Shared.Application.Results;

namespace Cards.Application.Commands.DeleteCard;

public class DeleteCardHandler(ICardRepository cardRepository) : IRequestHandler<DeleteCardCommand>
{
    public async Task<Result> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
    {
        Card? card = await cardRepository.GetById(request.Id, cancellationToken);
        if (card is null)
        {
            return Error.NotFound("Card not found");
        }
        await cardRepository.Delete(card, cancellationToken);
        return Result.Success();
    }
}