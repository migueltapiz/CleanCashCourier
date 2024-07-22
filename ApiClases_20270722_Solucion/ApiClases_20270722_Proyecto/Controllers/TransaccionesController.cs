using ApiClases_20270722_Proyecto.Repositorios;

namespace ApiClases_20270722_Proyecto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransaccionesController : ControllerBase{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(TransaccionRepositorioMemoria.Instancia.Transacciones);
    }
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(TransaccionRepositorioMemoria.Instancia.Transacciones.FirstOrDefault(transaccion => transaccion.Id == id, new Transaccion()));
    }
}
