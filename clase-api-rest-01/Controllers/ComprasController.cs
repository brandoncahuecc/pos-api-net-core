using clase_api_rest_01.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace clase_api_rest_01.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComprasController : ControllerBase
{
    private static List<Compra> Compras = new();

    public ComprasController() { }

    [HttpPost]
    public IActionResult CrearCompra(Compra compra)
    {
        compra.Id = Compras.Count + 1;
        compra.FechaEntrega = DateTime.Now.AddDays(5);
        decimal total = 0;
        int items = 0;

        compra.Productos.ForEach(x =>
        {
            total += x.Precio;
            items += x.Cantidad;
        });

        Compras.Add(compra);

        var respuesta = new
        {
            Total = total,
            CantidadItems = items
        };

        return Ok(respuesta);
    }

    [HttpGet]
    public IActionResult ObtenerCompra()
    {
        return BadRequest(Compras);
    }
}
