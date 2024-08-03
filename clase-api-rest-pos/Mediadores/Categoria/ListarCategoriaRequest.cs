using clase_api_rest_pos.Modelos;
using clase_api_rest_pos.Servicios;
using MediatR;

namespace clase_api_rest_pos.Mediadores.Categorias;

public class ListarCategoriaRequest : IRequest<List<Categoria>>
{

}

public class ListarCategoriaHandler : IRequestHandler<ListarCategoriaRequest, List<Categoria>>
{
    private readonly ICategoriaServicio _servicio;

    public ListarCategoriaHandler(ICategoriaServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<List<Categoria>> Handle(ListarCategoriaRequest request, CancellationToken cancellationToken)
    {
        var resultado = await _servicio.ObtenerCategorias();
        return resultado;
    }
}
