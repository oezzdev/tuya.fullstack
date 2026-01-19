using Shared.Domain.Entities;

namespace Shared.Domain.Ports;

public interface IRepository<T> where T : Entity
{
    Task Add(T entity, CancellationToken cancellationToken = default);
    Task Update(T entity, CancellationToken cancellationToken = default);
    Task Delete(T entity, CancellationToken cancellationToken = default);
    Task<T?> GetById(Guid id, CancellationToken cancellationToken = default);
}
