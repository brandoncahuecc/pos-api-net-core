namespace clase_api_rest_pos.Mediadores;

public static class Dependencias
{
    public static IServiceCollection AgregarMediadores(this IServiceCollection services)
    {
        return services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(Dependencias).Assembly));
    }
}
