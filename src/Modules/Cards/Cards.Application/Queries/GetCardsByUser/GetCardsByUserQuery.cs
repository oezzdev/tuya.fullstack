using Shared.Application.Cqrs;

namespace Cards.Application.Queries.GetCardsByUser;

public record GetCardsByUserQuery(Guid CustomerId) : IRequest<IEnumerable<GetCardsByUserResult>>;
