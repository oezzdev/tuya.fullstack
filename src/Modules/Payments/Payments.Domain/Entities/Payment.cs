using Shared.Domain.Entities;

namespace Payments.Domain.Entities;

public class Payment(Guid userId, Guid cardId, decimal amount) : Entity
{
    public Guid UserId { get; init; } = userId;
    public Guid CardId { get; init; } = cardId;
    public decimal Amount { get; init; } = amount;
    public DateTimeOffset Date { get; init; } = DateTimeOffset.UtcNow;
}
