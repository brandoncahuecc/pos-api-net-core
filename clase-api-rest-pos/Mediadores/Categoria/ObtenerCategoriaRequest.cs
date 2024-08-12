using clase_api_rest_resources.Modelos;
using clase_api_rest_resources.Modelos.Global;
using clase_api_rest_pos.Servicios;
using MediatR;

namespace clase_api_rest_pos.Mediadores.Categorias;

public class ObtenerCategoriaRequest : IRequest<Respuesta<Categoria>>
{
    public int Id { get; set; }
}

public class ObtenerCategoriaHandler : IRequestHandler<ObtenerCategoriaRequest, Respuesta<Categoria>>
{
    private readonly ICategoriaServicio _servicio;

    public ObtenerCategoriaHandler(ICategoriaServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<Respuesta<Categoria>> Handle(ObtenerCategoriaRequest request, CancellationToken cancellationToken)
    {
        var resultado = await _servicio.ObtenerCategoria(request.Id);
        return resultado;
    }
}