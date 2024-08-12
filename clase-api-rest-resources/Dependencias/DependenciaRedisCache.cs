using Microsoft.Extensions.DependencyInjection;

namespace clase_api_rest_resources.Dependencias;

public static class DependenciaRedisCache
{
    public static IServiceCollection AgregarRedisCache(this IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            string redisConnection = Environment.GetEnvironmentVariable("RedisConnection") ?? string.Empty;
            options.Configuration = redisConnection;
        });

        return services;
    }
}
