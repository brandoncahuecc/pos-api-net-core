using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace clase_api_rest_resources.Dependencias;

public static class DependenciaLogger
{
    public static ILoggingBuilder AgregarLogger(this ILoggingBuilder logging)
    {
        var loggerConfig = new LoggerConfiguration()
        .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("./Recursos/serilog-config.json").Build())
        .Enrich.FromLogContext().CreateLogger();

        logging.ClearProviders();
        logging.AddSerilog(loggerConfig);
        return logging;
    }
}
