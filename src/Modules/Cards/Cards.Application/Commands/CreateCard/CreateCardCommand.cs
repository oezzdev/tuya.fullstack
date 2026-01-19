using Shared.Application.Cqrs;

namespace Cards.Application.Commands.CreateCard;

public record CreateCardCommand : IRequest<CreatedCardResult>
{
    public Guid UserId { get; init; }
    public required string Number { get; init; }
    public required string HolderName { get; init; }
    public required DateTimeOffset ExpirationDate { get; init; }
    public required string Cvv { get; init; }
    public required decimal Balance { get; set; }
}
