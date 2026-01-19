namespace Cards.Application.Commands.CreateCard;

public record CreatedCardResult(Guid Id, Guid UserId, string Number, string HolderName, DateTimeOffset ExpirationDate, string Cvv, decimal Balance);
