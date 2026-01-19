using Shared.Application.Cqrs;

namespace Cards.Application.Queries.GetCardById;

public record GetCardByIdQuery(Guid Id) : IRequest<GetCardByIdResult>;
