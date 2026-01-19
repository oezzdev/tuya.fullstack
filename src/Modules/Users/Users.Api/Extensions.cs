using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure;

namespace Users.Api;

public static class Extensions
{
    /// <summary>
    /// Configures services required for the Users module and adds them to the application's dependency injection
    /// container.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance to configure. Cannot be null.</param>
    /// <returns>The same WebApplicationBuilder instance, to allow for method chaining.</returns>
    public static WebApplicationBuilder ConfigureUsersModule(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddPersistence(builder.Configuration)
            .AddAuth(builder.Configuration);

        return builder;
    }

    /// <summary>
    /// Adds users-related endpoints to the application's request pipeline.
    /// </summary>
    /// <remarks>Call this method during application startup to enable users features via HTTP endpoints.
    /// This method is typically used in the application's configuration pipeline.</remarks>
    /// <param name="application">The <see cref="WebApplication"/> instance to configure with users endpoints. Cannot be null.</param>
    /// <returns>The same <see cref="WebApplication"/> instance so that additional configuration can be chained.</returns>
    public static WebApplication UseUsersModule(this WebApplication application)
    {
        using (var scope = application.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<Infrastructure.Contexts.UsersContext>();
            context.Database.EnsureCreated();
        }
        application.MapControllers();
        return application;
    }
}
