using Cards.Domain.Ports;
using Cards.Infrastructure.Contexts;
using Cards.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cards.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<CardsContext>(options => options.UseSqlite(configuration.GetConnectionString("Cards")))
            .AddScoped<ICardRepository, CardRepository>();
    }
}
