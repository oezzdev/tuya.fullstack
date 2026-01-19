using Microsoft.EntityFrameworkCore;
using Shared.Domain.Entities;
using Shared.Domain.Ports;

namespace Shared.Infrastructure.Repositories;

public class Repository<T>(DbContext context) : IRepository<T> where T : Entity
{
    public async Task Add(T entity, CancellationToken cancellationToken = default)
    {
        await context.Set<T>().AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(T entity, CancellationToken cancellationToken = default)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<T?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Set<T>().FindAsync([id], cancellationToken).AsTask();
    }

    public async Task Update(T entity, CancellationToken cancellationToken = default)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
