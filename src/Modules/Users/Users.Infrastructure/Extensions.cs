using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Ports;
using Users.Domain.Ports;
using Users.Infrastructure.Contexts;
using Users.Infrastructure.Options;
using Users.Infrastructure.Repositories;
using Users.Infrastructure.Services;

namespace Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<UsersContext>(options => options.UseSqlite(configuration.GetConnectionString("Users")))
            .AddScoped<IUserRepository, UserRepository>();
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<IAuthenticator, Authenticator>()
            .AddSingleton<TokenGenerator>()
            .Configure<SignatureOptions>(configuration.GetRequiredSection(nameof(SignatureOptions)));
    }
}
