using clase_api_rest_pos.Modelos;
using clase_api_rest_pos.Servicios;
using MediatR;

namespace clase_api_rest_pos.Mediadores.Categorias;

public class CrearCategoriaRequest : IRequest<Categoria>
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public class CrearCategoriaHandler : IRequestHandler<CrearCategoriaRequest, Categoria>
{
    private readonly ICategoriaServicio _servicio;
    public CrearCategoriaHandler(ICategoriaServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<Categoria> Handle(CrearCategoriaRequest request, CancellationToken cancellationToken)
    {
        Categoria categoria = new()
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Condicion = true
        };

        var resultado = await _servicio.CrearCategoria(categoria);
        return resultado;
    }
}