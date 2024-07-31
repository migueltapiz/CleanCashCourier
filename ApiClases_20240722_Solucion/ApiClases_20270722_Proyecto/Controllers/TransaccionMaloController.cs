using ApiClases_20270722_Proyecto.Entidades;
using ApiClases_20270722_Proyecto.Repositorios;
using System.Reflection.Metadata.Ecma335;

namespace ApiClases_20270722_Proyecto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransaccionMaloController : ControllerBase
{

    public readonly ITransaccionRepositorioMalo repositorio;
    private readonly IMapper _mapper;
    public TransaccionMaloController(ITransaccionRepositorioMalo repositorio, IMapper mapper) {
        this.repositorio = repositorio;
        _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransaccionDto>>> GetAsync([FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin, [FromQuery] double? cantidadEnviadaMin, [FromQuery] double? cantidadEnviadaMax, [FromQuery] double? cantidadRecibidaMin, [FromQuery] double? cantidadRecibidaMax) {
        return Ok(_mapper.Map<IEnumerable<TransaccionDto>>(await repositorio.ObtenerTodosFiltrado(0, fechaInicio, fechaFin, cantidadEnviadaMin, cantidadEnviadaMax, cantidadRecibidaMin, cantidadRecibidaMax)));
    }

    [HttpGet("{id_transaccion}", Name = "getTransaccionMalo")]
    public ActionResult<TransaccionDto> Get(int id_transaccion) {
        var transaccion = repositorio.ObtenerTransaccionId(0, id_transaccion);
        var finalTransaccionDto = _mapper.Map<TransaccionDto>(transaccion);
        return finalTransaccionDto == null ? NotFound() : Ok(finalTransaccionDto);
    }



}