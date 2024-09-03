
using Microsoft.AspNetCore.Cors;


namespace ApiClases_20270722_Proyecto.Controllers
{
    [Route("api/Clientes/{id_cliente}/Transacciones")]
    [ApiController]
    public class TransaccionesController : ControllerBase
    {
        private readonly IRepositorioGenerico<Transaccion> _repositorio;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TransaccionesController(
            IRepositorioGenerico<Transaccion> repositorio,
            IMapper mapper,
            IMediator mediator)
        {
            _repositorio = repositorio;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransaccionGetDto>>> GetAsync([FromRoute] int id_cliente, [FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin, [FromQuery] double? cantidadEnviadaMin, [FromQuery] double? cantidadEnviadaMax, [FromQuery] double? cantidadRecibidaMin, [FromQuery] double? cantidadRecibidaMax)
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

            return Ok(_mapper.Map<IEnumerable<TransaccionGetDto>>(await _repositorio.ObtenerTodosFiltrado(filtro)));
        }

        [HttpGet("{id_transaccion}", Name = "getTransaccion")]
        public ActionResult<TransaccionGetDto> Get([FromRoute] int id_cliente, [FromRoute] int id_transaccion)
        {
            var transaccion = _repositorio.ObtenerTransaccionId(id_cliente, id_transaccion);

            if (transaccion == null)
            {
                return NotFound();
            }

            var finalTransaccionGetDto = _mapper.Map<TransaccionGetDto>(transaccion);
            return finalTransaccionGetDto == null ? NotFound() : Ok(finalTransaccionGetDto);
        }

        [EnableCors("AllowAllOrigins")]
        [HttpPost]
        public async Task<ActionResult<TransaccionPostDto>> Post(TransaccionPostDto transaccion)
        {
            var transaccionEntidad = _mapper.Map<Transaccion>(transaccion);
            _repositorio.Agregar(transaccionEntidad);

            if (await _repositorio.GuardarCambios())
            {
                var transaccionDto = _mapper.Map<TransaccionPostDto>(transaccionEntidad);


                // Enviar los datos de la transacción utilizando el mediador. SignalR.
                var resultado = await _mediator.Send(new SignalRRequest
                {
                    MandamosTransaccion = transaccionDto,
                    TipoAcceso = "Transaccion",

                });

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
        }
    }

    // No usamos ni PUT ni DELETE. No se pueden ni modificar ni eliminar transacciones.

}
