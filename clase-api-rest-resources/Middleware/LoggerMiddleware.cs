using clase_api_rest_resources.Modelos.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace clase_api_rest_resources.Middleware;

public class LoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggerMiddleware> _logger;

    public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation("LoggerMiddleware request: " + context.Request.Method + " " + context.Request.Path);
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Excepcion general");

            RespuestaDetalle detalle = new()
            {
                Codigo = "E-GLO",
                Mensaje = "Verifique con el área de soporte debido a que hay un error global",
                DetalleTecnico = ex.ToString()
            };

            var respuesta = context.Response;
            respuesta.StatusCode = 500;
            respuesta.ContentType = "application/json";
            await respuesta.WriteAsync(JsonConvert.SerializeObject(detalle));
        }

        _logger.LogInformation("LoggerMiddleware response: " + context.Response.StatusCode);
    }
}
