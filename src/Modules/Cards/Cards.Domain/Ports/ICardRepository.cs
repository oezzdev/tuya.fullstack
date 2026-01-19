using Cards.Domain.Entities;
using Shared.Domain.Ports;

namespace Cards.Domain.Ports;

public interface ICardRepository : IRepository<Card>
{
    Task<List<Card>> GetByUser(Guid userId, CancellationToken cancellationToken = default);
}