using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Payments.Infrastructure;
using Payments.Infrastructure.Contexts;

namespace Payments.Api;

public static class Extensions
{
    /// <summary>
    /// Configures services required for the Payments module and adds them to the application's dependency injection
    /// container.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance to configure. Cannot be null.</param>
    /// <returns>The same WebApplicationBuilder instance, to allow for method chaining.</returns>
    public static WebApplicationBuilder ConfigurePaymentsModule(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddPersistence(builder.Configuration)
            .AddServices();
        return builder;
    }

    /// <summary>
    /// Adds payments-related endpoints to the application's request pipeline.
    /// </summary>
    /// <remarks>Call this method during application startup to enable payments features via HTTP endpoints.
    /// This method is typically used in the application's configuration pipeline.</remarks>
    /// <param name="application">The <see cref="WebApplication"/> instance to configure with payments endpoints. Cannot be null.</param>
    /// <returns>The same <see cref="WebApplication"/> instance so that additional configuration can be chained.</returns>
    public static WebApplication UsePaymentsModule(this WebApplication application)
    {
        using (var scope = application.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            context.Database.EnsureCreated();
        }
        application.MapControllers();
        return application;
    }
}
