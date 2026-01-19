using Cards.Domain.Enums;
using Shared.Domain.Entities;

namespace Cards.Domain.Entities;

public sealed class Card(Guid userId, string number, string holderName, DateTimeOffset expirationDate, string cvv, decimal balance) : Entity
{
    public Guid UserId { get; init; } = userId;
    public string Number { get; init; } = number;
    public string HolderName { get; set; } = holderName;
    public DateTimeOffset ExpirationDate { get; init; } = expirationDate;
    public string Cvv { get; init; } = cvv;
    public decimal Balance { get; set; } = balance;
    public CardStatus Status { get; set; }
}
