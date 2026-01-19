using System.Reflection;

namespace Shared.Application.Cqrs;

public record MediatorOptions
{
    public Assembly[]? Locations { get; set; }

    public static MediatorOptions Default => new();
}