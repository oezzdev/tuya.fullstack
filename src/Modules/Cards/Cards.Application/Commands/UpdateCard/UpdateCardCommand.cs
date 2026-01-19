using Cards.Domain.Enums;
using Shared.Application.Cqrs;

namespace Cards.Application.Commands.UpdateCard;

public record UpdateCardCommand : IRequest
{
    public required Guid Id { get; init; }
    public required string HolderName { get; init; }
    public required CardStatus Status { get; init; }
}
