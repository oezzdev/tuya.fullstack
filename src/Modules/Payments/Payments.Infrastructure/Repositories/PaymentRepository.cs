using Microsoft.EntityFrameworkCore;
using Payments.Domain.Entities;
using Payments.Domain.Ports;
using Payments.Infrastructure.Contexts;
using Shared.Infrastructure.Repositories;

namespace Payments.Infrastructure.Repositories;

public class PaymentRepository(PaymentsContext context) : Repository<Payment>(context), IPaymentRepository
{
    private readonly PaymentsContext context = context;

    public Task<List<Payment>> GetByUser(Guid UserId, CancellationToken cancellationToken = default)
    {
        return context.Payments
            .Where(p => p.UserId == UserId)
            .ToListAsync(cancellationToken);
    }
}
