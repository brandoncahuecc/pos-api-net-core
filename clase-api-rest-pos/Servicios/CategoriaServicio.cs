using clase_api_rest_pos.Modelos;
using clase_api_rest_pos.Modelos.Global;
using clase_api_rest_pos.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace clase_api_rest_pos.Servicios;

public class CategoriaServicio : ICategoriaServicio
{
    //private readonly PosDbContext _context;
    private readonly ICategoriaPersistencia _persistencia;
    private readonly IDistributedCache _distributedCache;

    //public CategoriaServicio(PosDbContext context)
    public CategoriaServicio(ICategoriaPersistencia persistencia, IDistributedCache distributedCache)
    {
        _persistencia = persistencia;
        _distributedCache = distributedCache;
    }

    public async Task<Respuesta<Categoria>> ActualizarCategoria(Categoria request)
    {
        var result = await _persistencia.ActualizarCategoria(request);
        return result;
    }

    public async Task<Respuesta<Categoria>> CrearCategoria(Categoria request)
    {
        var result = await _persistencia.CrearCategoria(request);
        return result;
    }

    public async Task<Respuesta<RespuestaDetalle>> EliminarCategoria(int id)
    {
        var result = await _persistencia.EliminarCategoria(id);
        return result;
    }

    public async Task<Respuesta<Categoria>> ObtenerCategoria(int id)
    {
        var result = await _persistencia.ObtenerCategoria(id);
        return result;
    }

    public async Task<Respuesta<List<Categoria>>> ObtenerCategorias()
    {
        try
        {
            string categoriasCache = await _distributedCache.GetStringAsync("Categorias") ?? string.Empty;

            if (!string.IsNullOrEmpty(categoriasCache))
            {
                var categorias = JsonConvert.DeserializeObject<List<Categoria>>(categoriasCache);
                return new(categorias);
            }
        }
        catch (Exception ex)
        {
            return new("E-RED-CON", ex);
        }

        var result = await _persistencia.ObtenerCategorias();

        DistributedCacheEntryOptions options = new();
        options.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

        try
        {
            if (result.Resultado)
                await _distributedCache.SetStringAsync("Categorias", JsonConvert.SerializeObject(result.Objeto), options);
        }
        catch (Exception ex)
        {
            return new("E-RED-CON", ex);
        }

        await Task.Delay(5000);

        return result;
    }

    // public async Task<Categoria> ActualizarCategoria(Categoria request)
    // {
    //     var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == request.Id);
    //     if (categoria is null)
    //         return null;

    //     categoria.Nombre = request.Nombre;
    //     categoria.Descripcion = request.Descripcion;
    //     categoria.Condicion = request.Condicion;

    //     var cantActualizado = await _context.SaveChangesAsync();

    //     if (cantActualizado > 0)
    //         return categoria;

    //     return null;
    // }

    // public async Task<Categoria> CrearCategoria(Categoria request)
    // {
    //     await _context.AddAsync(request);
    //     var cantInsertado = await _context.SaveChangesAsync();

    //     if (cantInsertado > 0)
    //         return request;

    //     return null;
    // }

    // public async Task<bool> EliminarCategoria(int id)
    // {
    //     var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
    //     if (categoria is null)
    //         return false;

    //     _context.Categorias.Remove(categoria);
    //     var cantEliminados = await _context.SaveChangesAsync();

    //     if (cantEliminados > 0)
    //         return true;

    //     return false;
    // }

    // public async Task<Categoria> ObtenerCategoria(int id)
    // {
    //     var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
    //     return categoria;
    // }

    // public async Task<List<Categoria>> ObtenerCategorias()
    // {
    //     var categorias = await _context.Categorias.ToListAsync();
    //     return categorias;
    // }
}

public interface ICategoriaServicio
{
    Task<Respuesta<List<Categoria>>> ObtenerCategorias();
    Task<Respuesta<Categoria>> ObtenerCategoria(int id);
    Task<Respuesta<Categoria>> CrearCategoria(Categoria request);
    Task<Respuesta<Categoria>> ActualizarCategoria(Categoria request);
    Task<Respuesta<RespuestaDetalle>> EliminarCategoria(int id);
}