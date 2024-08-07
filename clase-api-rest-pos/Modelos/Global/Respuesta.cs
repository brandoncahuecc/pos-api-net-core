namespace clase_api_rest_pos.Modelos.Global;

public class Respuesta<T>
{
    public bool Resultado { get; set; }
    public T Objeto { get; set; }
    public RespuestaDetalle Detalle { get; set; }

    public Respuesta(T objeto)
    {
        Resultado = true;
        Objeto = objeto;
    }

    public Respuesta(string codigo, Exception exception)
    {
        Resultado = false;
        Detalle = new()
        {
            Codigo = codigo,
            Mensaje = exception.Message,
            DetalleTecnico = exception.ToString()
        };
    }

    public Respuesta(bool resultado, string codigo, string mensaje)
    {
        Resultado = resultado;
        Detalle = new()
        {
            Codigo = codigo,
            Mensaje = mensaje
        };
    }
}
