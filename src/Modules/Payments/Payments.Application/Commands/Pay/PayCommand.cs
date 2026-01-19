using Shared.Application.Cqrs;

namespace Payments.Application.Commands.Pay;

public record PayCommand(Guid UserId, Guid CardId, decimal Amount) : IRequest;
