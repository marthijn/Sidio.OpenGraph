using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Sidio.OpenGraph;

/// <summary>
/// The service collection extensions class.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Open Graph services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddOpenGraph(this IServiceCollection services)
    {
        services.AddSingleton<IOpenGraphBuilderFactory, OpenGraphBuilderFactory>();
        return services;
    }
}