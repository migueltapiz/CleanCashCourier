using ApiClases_20270722_Proyecto.Entidades;
using ApiClases_20270722_Proyecto.Repositorios;
using Microsoft.AspNetCore.Cors;
using System.Reflection.Metadata.Ecma335;

namespace ApiClases_20270722_Proyecto.Controllers;

//[Route("api/[controller]")]
[Route("api/Clientes/{id_cliente}/Transacciones")]

[ApiController]
public class TransaccionesController : ControllerBase{

    public readonly IRepositorioGenerico<Transaccion> repositorio;
    private readonly IMapper _mapper;
    public TransaccionesController(IRepositorioGenerico<Transaccion> repositorio, IMapper mapper) {
        this.repositorio = repositorio;
        _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransaccionDto>>> GetAsync([FromRoute]int id_cliente,[FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin, [FromQuery] double? cantidadEnviadaMin, [FromQuery] double? cantidadEnviadaMax, [FromQuery] double? cantidadRecibidaMin, [FromQuery] double? cantidadRecibidaMax) {
        return Ok(_mapper.Map<IEnumerable<TransaccionDto>>(await repositorio.ObtenerTodosFiltrado(id_cliente,fechaInicio,fechaFin,cantidadEnviadaMin, cantidadEnviadaMax, cantidadRecibidaMin, cantidadRecibidaMax)));
    }


    [HttpGet("{id_transaccion}",  Name = "getTransaccion") ]
    public ActionResult<TransaccionGetDto> Get([FromRoute] int id_cliente, [FromRoute] int id_transaccion)
    {
        var transaccion = repositorio.ObtenerTransaccionId(id_cliente, id_transaccion);

        // Si la transacción es nula, devolvemos un 404 Not Found
        if (transaccion == null)
        {
            return NotFound();
        }

        var finalTransaccionDto = _mapper.Map<TransaccionGetDto>(transaccion);

        // Si el mapeo falla y finalTransaccionDto es null (aunque esto no debería suceder si transaccion no es null)
        return finalTransaccionDto == null ? NotFound() : Ok(finalTransaccionDto);
    }



    [EnableCors("AllowAllOrigins")]
    [HttpPost]
    public async Task<ActionResult<TransaccionPostDto>> Post(TransaccionPostDto transaccion)
    {
        var finalTransaccionNuevo = _mapper.Map<TransaccionPostDto, Transaccion>(transaccion);
        repositorio.Agregar(finalTransaccionNuevo);

        if (await repositorio.GuardarCambios())
        {
            // Suponiendo que el Id de la nueva transacción está disponible en finalTransaccionNuevo.Id
            return CreatedAtAction(
                nameof(Get),
                new { id_cliente = finalTransaccionNuevo.IdEnvia, id_transaccion = finalTransaccionNuevo.Id },
                _mapper.Map<TransaccionPostDto>(finalTransaccionNuevo)
            );
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<ActionResult<TransaccionPutDto>> PutAsync(int id, TransaccionPutDto transaccion) {
        var finalTransaccionActualizado = _mapper.Map<Transaccion>(transaccion);
        repositorio.Actualizar(id, finalTransaccionActualizado);
        // Devuelve un NoContent si la actualización fue exitosa
        return await repositorio.GuardarCambios() ? NoContent() : BadRequest();
    }

    [HttpDelete]
    public async Task<ActionResult<TransaccionDeleteDto>> DeleteAsync(int id) {
        repositorio.Borrar(id);
        // Devuelve un NoContent si la eliminación fue exitosa
        return await repositorio.GuardarCambios() ? NoContent() : BadRequest();
    }

    

}
