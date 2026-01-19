using Shared.Application.Cqrs;

namespace Payments.Application.Queries.GetPaymentsByUser;

public record GetPaymentsByUserQuery(Guid UserId) : IRequest<IEnumerable<GetPaymentsByUserResult>>;
