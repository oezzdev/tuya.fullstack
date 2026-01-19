using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;
using Users.Application.Ports;
using Users.Domain.Entities;
using Users.Domain.Ports;
using Users.Infrastructure.Contexts;

namespace Users.Infrastructure.Repositories;

public sealed class UserRepository(UsersContext context, IAuthenticator authenticator) : Repository<User>(context), IUserRepository
{
    private readonly UsersContext context = context;

    public async Task<User?> GetByCredentials(string username, string password, CancellationToken cancellationToken = default)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
        if (user is null)
        {
            return null;
        }

        if (!authenticator.Verify(user.Password, password))
        {
            return null;
        }

        return user;
    }
}
