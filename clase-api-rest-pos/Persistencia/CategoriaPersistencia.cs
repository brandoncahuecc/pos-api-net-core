using System.Text;
using clase_api_rest_resources.Modelos;
using clase_api_rest_resources.Modelos.Global;
using Dapper;
using MySql.Data.MySqlClient;

namespace clase_api_rest_pos.Persistencia;

public class CategoriaPersistencia : ICategoriaPersistencia
{
    private readonly string _stringConnection;

    public CategoriaPersistencia()
    {
        _stringConnection = Environment.GetEnvironmentVariable("StringConnection") ?? string.Empty;
    }

    public async Task<Respuesta<Categoria>> ActualizarCategoria(Categoria request)
    {
        using (MySqlConnection conn = new MySqlConnection(_stringConnection))
        {
            Respuesta<Categoria> respuesta;

            try
            {
                await conn.OpenAsync();
                StringBuilder query = new("UPDATE categoria ");
                query.Append("SET nombre = @Nombre, ");
                query.Append("descripcion = @Descripcion, ");
                query.Append("condicion = @Condicion ");
                query.Append("WHERE idcategoria = @IdCategoria");

                DynamicParameters parameters = new();

                parameters.Add("Nombre", request.Nombre);
                parameters.Add("Descripcion", request.Descripcion);
                parameters.Add("Condicion", request.Condicion ? 1 : 0);
                parameters.Add("IdCategoria", request.IdCategoria);

                var result = await conn.ExecuteAsync(query.ToString(), parameters);
                if (result > 0)
                    respuesta = new(request);
                else
                    respuesta = new(false, "E-NO-UPD", "No fue posible actaulizar la categoria");
            }
            catch (Exception ex)
            {
                respuesta = new("E-DB", ex);
            }

            return respuesta;
        }
    }

    public async Task<Respuesta<Categoria>> CrearCategoria(Categoria request)
    {
        using (MySqlConnection conn = new MySqlConnection(_stringConnection))
        {
            Respuesta<Categoria> respuesta;

            try
            {
                await conn.OpenAsync();
                StringBuilder query = new("INSERT INTO categoria ");
                query.Append("(nombre, descripcion, condicion) ");
                query.Append("VALUES (@Nombre, @Descripcion, @Condicion)");

                DynamicParameters parameters = new();

                parameters.Add("Nombre", request.Nombre);
                parameters.Add("Descripcion", request.Descripcion);
                parameters.Add("Condicion", 1);

                var result = await conn.ExecuteAsync(query.ToString(), parameters);

                if (result > 0)
                    respuesta = new(request);
                else
                    respuesta = new(false, "E-NO-ADD", "No fue posible crear la categoria");
            }
            catch (Exception ex)
            {
                respuesta = new("E-DB", ex);
            }

            return respuesta;
        }
    }

    public async Task<Respuesta<RespuestaDetalle>> EliminarCategoria(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(_stringConnection))
        {
            Respuesta<RespuestaDetalle> respuesta;

            try
            {
                await conn.OpenAsync();
                var result = await conn.ExecuteAsync("DELETE FROM categoria WHERE idcategoria = @IdCategoria", new { IdCategoria = id });
                if (result > 0)
                    respuesta = new(true, "S-DEL", $"Categoria con id '{id}' eliminada exitosamente");
                else
                    respuesta = new(false, "E-NO-DEL", $"Categoria con id '{id}' no existe y no fue posible eliminala");
            }
            catch (Exception ex)
            {
                respuesta = new("E-DB", ex);
            }

            return respuesta;
        }
    }

    public async Task<Respuesta<Categoria>> ObtenerCategoria(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(_stringConnection))
        {
            Respuesta<Categoria> respuesta;

            try
            {
                await conn.OpenAsync();
                var result = await conn.QueryAsync<Categoria>("SELECT * FROM categoria WHERE condicion = 1 AND idcategoria = @IdCategoria", new { IdCategoria = id });

                if (result is null || result.Count() == 0)
                    respuesta = new(false, "E-DB-NF", $"No se encontro la categoria con id '{id}'");
                else
                    respuesta = new(result.FirstOrDefault() ?? new());
            }
            catch (Exception ex)
            {
                respuesta = new("E-DB", ex);
            }

            return respuesta;
        }
    }

    public async Task<Respuesta<List<Categoria>>> ObtenerCategorias()
    {
        using (MySqlConnection conn = new MySqlConnection(_stringConnection))
        {
            Respuesta<List<Categoria>> respuesta;

            try
            {
                await conn.OpenAsync();
                var result = await conn.QueryAsync<Categoria>("SELECT * FROM categoria WHERE condicion = 1");

                respuesta = new(result.ToList() ?? new());
            }
            catch (Exception ex)
            {
                respuesta = new("E-DB", ex);
            }

            return respuesta;
        }
    }
}

public interface ICategoriaPersistencia
{
    Task<Respuesta<List<Categoria>>> ObtenerCategorias();
    Task<Respuesta<Categoria>> ObtenerCategoria(int id);
    Task<Respuesta<Categoria>> CrearCategoria(Categoria request);
    Task<Respuesta<Categoria>> ActualizarCategoria(Categoria request);
    Task<Respuesta<RespuestaDetalle>> EliminarCategoria(int id);
}
