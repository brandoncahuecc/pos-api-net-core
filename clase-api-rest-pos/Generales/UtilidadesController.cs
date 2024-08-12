using clase_api_rest_resources.Modelos.Global;
using Microsoft.AspNetCore.Mvc;

namespace clase_api_rest_pos.Generales;

public class UtilidadesController : ControllerBase
{
    public IActionResult Result<T>(Respuesta<T> respuesta)
    {
        if (respuesta.Resultado)
            return Ok(respuesta.Objeto);
        else
            return BadRequest(respuesta.Detalle);
    }
}
