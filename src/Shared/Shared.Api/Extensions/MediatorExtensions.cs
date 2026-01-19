using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Api.Cqrs;
using Shared.Application.Cqrs;
using System.Reflection;

namespace Shared.Api.Extensions;

public static class MediatorExtensions
{
    /// <summary>
    /// Registers the mediator and all request handler implementations with the dependency injection container.
    /// </summary>
    /// <remarks>This method scans all loaded assemblies for types implementing <see
    /// cref="IRequestHandler{TRequest}"/> or <see cref="IRequestHandler{TRequest, TResponse}"/> and registers them as
    /// scoped services. Call this method during application startup to enable mediator-based request
    /// handling.</remarks>
    /// <param name="services">The service collection to which the mediator and handlers will be added.</param>
    /// <param name="options">Optional configuration options for the mediator.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
    public static IServiceCollection AddMediator(this IServiceCollection services, Action<MediatorOptions>? optionsBuilder = default)
    {
        MediatorOptions options = MediatorOptions.Default;
        optionsBuilder?.Invoke(options);
        Assembly[] assemblies = options.Locations ?? AppDomain.CurrentDomain.GetAssemblies();

        services.AddScoped<IMediator, Mediator>();
        services.AddValidatorsFromAssemblies(assemblies);

        Type requestHandlerType = typeof(IRequestHandler<>);
        Type requestHandlerType2 = typeof(IRequestHandler<,>);
        IEnumerable<Type> requestHandlerTypes = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType && (i.GetGenericTypeDefinition() == requestHandlerType || i.GetGenericTypeDefinition() == requestHandlerType2)) && !type.IsAbstract && !type.IsInterface);

        foreach (Type handlerType in requestHandlerTypes)
        {
            Type[] interfaceTypes = [.. handlerType.GetInterfaces().Where(i => i.IsGenericType && i.IsGenericType && (i.GetGenericTypeDefinition() == requestHandlerType || i.GetGenericTypeDefinition() == requestHandlerType2))];
            foreach (Type interfaceType in interfaceTypes)
            {
                services.AddScoped(interfaceType, handlerType);
            }
        }

        return services;
    }
}
