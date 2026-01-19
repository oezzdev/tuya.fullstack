namespace Cards.Application.Queries.GetCardById;

public record GetCardByIdResult(Guid Id, Guid UserId, string Number, string HolderName, DateTimeOffset ExpirationDate, string Cvv, decimal Balance);