namespace Payments.Application.Queries.GetPaymentsByUser;

public record GetPaymentsByUserResult(Guid Id, Guid CardId, decimal Amount, DateTimeOffset Date);
