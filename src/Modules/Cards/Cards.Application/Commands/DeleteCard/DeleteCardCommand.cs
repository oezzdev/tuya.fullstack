using Shared.Application.Cqrs;

namespace Cards.Application.Commands.DeleteCard;

public record DeleteCardCommand(Guid Id) : IRequest;
