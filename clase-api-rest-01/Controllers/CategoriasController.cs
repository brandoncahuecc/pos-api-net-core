using clase_api_rest_01.Models;
using Microsoft.AspNetCore.Mvc;

namespace clase_api_rest_01;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private static List<Categoria> Categorias = new();

    [HttpGet]
    public IActionResult Obtener()
    {
        return Ok(Categorias.Where(c => !c.Eliminado));
    }

    [HttpPost]
    public IActionResult Crear(Categoria categoria)
    {
        if (ValidarExisteCategoria(categoria.Nombre))
        {
            categoria.Id = Categorias.Count + 1;
            Categorias.Add(categoria);
            return Ok(categoria);
        }
        else
        {
            var response = new { Mensaje = "El nombre de la categoria ya existe" };
            return BadRequest(response);
        }
    }

    private bool ValidarExisteCategoria(string nombre)
    {
        Categoria categoria = Categorias.FirstOrDefault(c => c.Nombre.Equals(nombre) && !c.Eliminado);

        if (categoria is null)
            return true;
        else
            return false;
    }

    [HttpPut("{id}")]
    public IActionResult Actualizar(int id, Categoria categoria)
    {
        Categoria temp = Categorias.FirstOrDefault(c => c.Id == id && !c.Eliminado);

        if (temp is null)
            return BadRequest(new { Mensaje = "La categoria que intenta actualizar, no existe" });

        Categorias.ForEach(c =>
        {
            if (c.Id == id)
            {
                c.Nombre = categoria.Nombre;
                c.Descripcion = categoria.Descripcion;
                c.Condicion = categoria.Condicion;
                c.Eliminado = categoria.Eliminado;
            }
        });

        return Ok(temp);
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        Categoria temp = Categorias.FirstOrDefault(c => c.Id == id && !c.Eliminado);

        if (temp is null)
            return BadRequest(new { Mensaje = "La categoria que intenta actualizar, no existe" });

        Categorias.ForEach(c =>
        {
            if (c.Id == id)
                c.Eliminado = true;
        });

        return Ok(new { Mensaje = "La categoria ha sido eliminado correctamente" });
    }

}
