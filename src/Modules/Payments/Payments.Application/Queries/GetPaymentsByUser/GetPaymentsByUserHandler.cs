using Payments.Domain.Entities;
using Payments.Domain.Ports;
using Shared.Application.Cqrs;
using Shared.Application.Results;

namespace Payments.Application.Queries.GetPaymentsByUser;

public class GetPaymentsByUserHandler(IPaymentRepository paymentRepository) : IRequestHandler<GetPaymentsByUserQuery, IEnumerable<GetPaymentsByUserResult>>
{
    public async Task<Result<IEnumerable<GetPaymentsByUserResult>>> Handle(GetPaymentsByUserQuery request, CancellationToken cancellationToken = default)
    {
        List<Payment> payments = await paymentRepository.GetByUser(request.UserId, cancellationToken);

        return payments
            .Select(p => new GetPaymentsByUserResult(p.Id, p.CardId, p.Amount, p.Date))
            .ToList();
    }
}