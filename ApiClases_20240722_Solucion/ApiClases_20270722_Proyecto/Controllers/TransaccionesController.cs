using ApiClases_20270722_Proyecto.Modelos.Transacciones;
using ApiClases_20270722_Proyecto.Repositorios;
using Microsoft.AspNetCore.Cors;

namespace ApiClases_20270722_Proyecto.Controllers;


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
    public async Task<ActionResult<IEnumerable<TransaccionGetDto>>> GetAsync([FromRoute] int id_cliente,[FromQuery] DateTime? fechaInicio,[FromQuery] DateTime? fechaFin,[FromQuery] double? cantidadEnviadaMin,[FromQuery] double? cantidadEnviadaMax,[FromQuery] double? cantidadRecibidaMin,[FromQuery] double? cantidadRecibidaMax)
    {
        var filtro = new FiltroTransacciones
        {
            IdCliente = id_cliente,
            FechaInicio = fechaInicio,
            FechaFin = fechaFin,
            CantidadEnviadaMin = cantidadEnviadaMin,
            CantidadEnviadaMax = cantidadEnviadaMax,
            CantidadRecibidaMin = cantidadRecibidaMin,
            CantidadRecibidaMax = cantidadRecibidaMax
        };

        return Ok(_mapper.Map<IEnumerable<TransaccionGetDto>>(await repositorio.ObtenerTodosFiltrado(filtro)));
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

        var finalTransaccionGetDto = _mapper.Map<TransaccionGetDto>(transaccion);

        // Si el mapeo falla y finalTransaccionGetDto es null (aunque esto no debería suceder si transaccion no es null)
        return finalTransaccionGetDto == null ? NotFound() : Ok(finalTransaccionGetDto);
    }



    [EnableCors("AllowAllOrigins")]
    [HttpPost]
    public async Task<ActionResult<TransaccionPostDto>> Post(TransaccionPostDto transaccion)
    {
        var transaccionEntidad = _mapper.Map<Transaccion>(transaccion);
        repositorio.Agregar(transaccionEntidad);

        if (await repositorio.GuardarCambios())
        {
            var transaccionDto = _mapper.Map<TransaccionPostDto>(transaccionEntidad);
            return CreatedAtAction(
                nameof(Get),
                new { id_cliente = transaccionEntidad.IdEnvia, id_transaccion = transaccionEntidad.Id },
                transaccionDto
            );
        }
        else
        {
            return BadRequest();
        }

        /* var finalTransaccionNuevo = _mapper.Map<TransaccionPostDto, Transaccion>(transaccion);
        repositorio.Agregar(finalTransaccionNuevo);

        if (await repositorio.GuardarCambios())
        {            
            return CreatedAtAction(
                nameof(Get),
                new { id_cliente = finalTransaccionNuevo.IdEnvia, id_transaccion = finalTransaccionNuevo.Id },
                _mapper.Map<TransaccionPostDto>(finalTransaccionNuevo)
            );
        }
        else
        {
            return BadRequest();
        } */
    }


    // No usamos ni Put ni Delete


}
