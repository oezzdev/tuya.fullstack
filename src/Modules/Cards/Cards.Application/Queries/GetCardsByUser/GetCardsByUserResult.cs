using Cards.Domain.Enums;

namespace Cards.Application.Queries.GetCardsByUser;

public record GetCardsByUserResult(
    Guid Id,
    Guid UserId,
    string Number,
    string HolderName,
    DateTimeOffset ExpirationDate,
    string Cvv,
    decimal Balance,
    CardStatus Status);