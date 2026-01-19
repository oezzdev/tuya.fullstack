using Payments.Application.Ports;
using Payments.Domain.Entities;
using Payments.Domain.Ports;
using Shared.Application.Cqrs;
using Shared.Application.Results;

namespace Payments.Application.Commands.Pay;

public class PayHandler(ICardsService cardsService, IPaymentRepository paymentRepository) : IRequestHandler<PayCommand>
{
    public async Task<Result> Handle(PayCommand request, CancellationToken cancellationToken = default)
    {
        Result discountResult = await cardsService.Discount(request.CardId, request.Amount, cancellationToken);
        if (!discountResult.IsSuccess)
        {
            return discountResult.Error;
        }

        Payment payment = new(request.UserId, request.CardId, request.Amount);
        await paymentRepository.Add(payment, cancellationToken);
        return Result.Success();
    }
}
