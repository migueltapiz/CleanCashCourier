using ApiClases_20270722_Proyecto.Repositorios;
using System.Reflection.Metadata.Ecma335;

namespace ApiClases_20270722_Proyecto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransaccionesController : ControllerBase{
    [HttpGet]
    public ActionResult<IEnumerable<TransaccionDto>> Get(){
        return Ok(TransaccionRepositorioMemoria.Instancia.Transacciones);
    }

    [HttpGet("{id}")]
    public ActionResult<TransaccionDto> Get(int id){
        var transaccion = TransaccionRepositorioMemoria.Instancia.Transacciones.FirstOrDefault(transaccion => transaccion.Id == id);
        return transaccion == null ? NotFound(): Ok(transaccion);
    }
}
