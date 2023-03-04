using Microsoft.Extensions.DependencyInjection;

namespace Hackathon2023.Cache;

public static class CacheExtensions
{
    /// <summary>
    /// Adds Caches
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCaches(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICallCache, CallCache>();
      
        return services;
    }
}