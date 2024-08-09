using clase_api_rest_pos.Modelos;
using clase_api_rest_pos.Modelos.Global;
using clase_api_rest_pos.Servicios;
using MediatR;

namespace clase_api_rest_pos.Mediadores.Categorias;

public class ListarCategoriaRequest : IRequest<Respuesta<List<Categoria>>>
{

}

public class ListarCategoriaHandler : IRequestHandler<ListarCategoriaRequest, Respuesta<List<Categoria>>>
{
    private readonly ICategoriaServicio _servicio;
    private readonly ILogger<ListarCategoriaHandler> _logger;

    public ListarCategoriaHandler(ICategoriaServicio servicio, ILogger<ListarCategoriaHandler> logger)
    {
        _servicio = servicio;
        _logger = logger;
    }

    public async Task<Respuesta<List<Categoria>>> Handle(ListarCategoriaRequest request, CancellationToken cancellationToken)
    {
        int num = 0;
        var resultadoDiv = 5 / num;
        _logger.LogCritical("Log Critical");
        _logger.LogDebug("Log Debug");
        _logger.LogError("Log Error");
        _logger.LogInformation("Log Information", new { Mensaje = "Logs de ILogger", Resultado = false });
        _logger.LogTrace("Log Trace", new { Mensaje = "Logs de ILogger", Resultado = false });
        _logger.LogWarning("Log Warning", new { Mensaje = "Logs de ILogger", Resultado = false });
        var resultado = await _servicio.ObtenerCategorias();
        return resultado;
    }
}
