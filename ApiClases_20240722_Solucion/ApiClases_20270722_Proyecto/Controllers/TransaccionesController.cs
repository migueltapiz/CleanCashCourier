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

    [HttpGet("{id}",  Name = "getTransaccion") ]
    public ActionResult<TransaccionDto> Get(int id){
        var transaccion = TransaccionRepositorioMemoria.Instancia.Transacciones.FirstOrDefault(transaccion => transaccion.Id == id);
        return transaccion == null ? NotFound(): Ok(transaccion);
    }

    [HttpPost]
    public ActionResult<TransaccionDto> Post(TransaccionDto transaccion) {

        int maxId = TransaccionRepositorioMemoria.Instancia.Transacciones.Max(t => t.Id);
        var finalTransaccion = new TransaccionDto()
        {
            Id = ++maxId,
            IdEnvia = transaccion.IdEnvia,
            IdRecibe = transaccion.IdRecibe,
            CantidadEnvia = transaccion.CantidadEnvia,
            CantidadRecibe = transaccion.CantidadRecibe,
            Fecha = transaccion.Fecha
        };
        TransaccionRepositorioMemoria.Instancia.Transacciones.Add(finalTransaccion);

        return CreatedAtRoute("getTransaccion",
                 new
                 {
                     id = finalTransaccion.Id
                 },
                 finalTransaccion);
    }
}
