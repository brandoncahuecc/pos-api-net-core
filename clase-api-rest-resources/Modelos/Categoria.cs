﻿namespace clase_api_rest_resources.Modelos;

public class Categoria
{
    public int IdCategoria { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Condicion { get; set; }
}
