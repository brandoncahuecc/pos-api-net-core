using clase_api_rest_pos.Generales;
using clase_api_rest_pos.Mediadores.Categorias;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace clase_api_rest_pos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : UtilidadesController
{
    private readonly IMediator _mediator;

    public CategoriasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerCategorias()
    {
        var resultado = await _mediator.Send(new ListarCategoriaRequest { });
        return Result(resultado);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerCategoria(int id)
    {
        var resultado = await _mediator.Send(new ObtenerCategoriaRequest { Id = id });
        return Result(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> CrearCategoria(CrearCategoriaRequest request)
    {
        var resultado = await _mediator.Send(request);
        return Result(resultado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarCategoria(int id, ActualizarCategoriaRequest request)
    {
        request.Id = id;
        var resultado = await _mediator.Send(request);
        return Result(resultado);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarCategoria(int id)
    {
        var resultado = await _mediator.Send(new EliminarCategoriaRequest { Id = id });
        return Result(resultado);
    }
}
