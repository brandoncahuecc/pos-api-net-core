using clase_api_rest_pos.Modelos;
using clase_api_rest_pos.Servicios;
using MediatR;

namespace clase_api_rest_pos.Mediadores.Categorias;

public class ActualizarCategoriaRequest : IRequest<Categoria>
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Condicion { get; set; }
}

public class ActualizarCategoriaHandler : IRequestHandler<ActualizarCategoriaRequest, Categoria>
{
    private readonly ICategoriaServicio _servicio;

    public ActualizarCategoriaHandler(ICategoriaServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<Categoria> Handle(ActualizarCategoriaRequest request, CancellationToken cancellationToken)
    {
        Categoria categoria = new()
        {
            Id = request.Id,
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Condicion = request.Condicion
        };

        var resultado = await _servicio.ActualizarCategoria(categoria);
        return resultado;
    }
}
