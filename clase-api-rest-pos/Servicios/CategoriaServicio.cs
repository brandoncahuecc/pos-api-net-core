using clase_api_rest_pos.Modelos;
using clase_api_rest_pos.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace clase_api_rest_pos.Servicios;

public class CategoriaServicio : ICategoriaServicio
{
    private readonly PosDbContext _context;

    public CategoriaServicio(PosDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria> ActualizarCategoria(Categoria request)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (categoria is null)
            return null;

        categoria.Nombre = request.Nombre;
        categoria.Descripcion = request.Descripcion;
        categoria.Condicion = request.Condicion;

        var cantActualizado = await _context.SaveChangesAsync();

        if (cantActualizado > 0)
            return categoria;

        return null;
    }

    public async Task<Categoria> CrearCategoria(Categoria request)
    {
        await _context.AddAsync(request);
        var cantInsertado = await _context.SaveChangesAsync();

        if (cantInsertado > 0)
            return request;

        return null;
    }

    public async Task<bool> EliminarCategoria(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        if (categoria is null)
            return false;

        _context.Categorias.Remove(categoria);
        var cantEliminados = await _context.SaveChangesAsync();

        if (cantEliminados > 0)
            return true;

        return false;
    }

    public async Task<Categoria> ObtenerCategoria(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        return categoria;
    }

    public async Task<List<Categoria>> ObtenerCategorias()
    {
        var categorias = await _context.Categorias.ToListAsync();
        return categorias;
    }
}

public interface ICategoriaServicio
{
    Task<List<Categoria>> ObtenerCategorias();
    Task<Categoria> ObtenerCategoria(int id);
    Task<Categoria> CrearCategoria(Categoria request);
    Task<Categoria> ActualizarCategoria(Categoria request);
    Task<bool> EliminarCategoria(int id);
}