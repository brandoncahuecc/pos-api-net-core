namespace clase_api_rest_01.Models;

public class Compra
{
    public int Id { get; set; }
    public string NombreProveedor { get; set; }
    public string DireccionEntrega { get; set; }
    public DateTime FechaEntrega { get; set; }
    public string NombreComprador { get; set; }
    public int Telefono { get; set; }
    public List<DetalleCompra> Productos { get; set; }
}
