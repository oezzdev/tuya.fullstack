using Shared.Domain.Ports;
using Users.Domain.Entities;

namespace Users.Domain.Ports;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByCredentials(string username, string password, CancellationToken cancellationToken = default);
}