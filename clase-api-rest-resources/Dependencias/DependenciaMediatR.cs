using Microsoft.Extensions.DependencyInjection;

namespace clase_api_rest_resources.Dependencias;

public static class DependenciaMediatR
{
    public static IServiceCollection AgregarMediador<T>(this IServiceCollection services)
    {
        return services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(T).Assembly));
    }
}
