using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payments.Application.Ports;
using Payments.Domain.Ports;
using Payments.Infrastructure.Contexts;
using Payments.Infrastructure.Repositories;
using Payments.Infrastructure.Services;

namespace Payments.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<PaymentsContext>(options => options.UseSqlite(configuration.GetConnectionString("Payments")))
            .AddScoped<IPaymentRepository, PaymentRepository>();
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<ICardsService, CardsService>();
    }
}
