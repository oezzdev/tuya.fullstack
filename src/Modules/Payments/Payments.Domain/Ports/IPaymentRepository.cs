using Payments.Domain.Entities;
using Shared.Domain.Ports;

namespace Payments.Domain.Ports;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<List<Payment>> GetByUser(Guid UserId, CancellationToken cancellationToken = default);
}
