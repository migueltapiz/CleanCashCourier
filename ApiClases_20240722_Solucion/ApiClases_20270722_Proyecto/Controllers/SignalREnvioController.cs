
namespace ApiClases_20270722_Proyecto.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SignalREnvioController : ControllerBase
{
    private readonly IMediator _mediator;

    public SignalREnvioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("enviarCliente")]
    public async Task<IActionResult> EnviarCliente([FromBody] ClienteBaseDto cliente, [FromQuery] string tipoAcceso)
    {
        if (cliente == null)
        {
            return BadRequest("El cliente no puede estar vacío.");
        }

        var resultado = await _mediator.Send(new SignalRRequest { MandamosCliente = cliente, TipoAcceso = tipoAcceso });
        return Ok(resultado);
    }

    [HttpPost("enviarTransaccion")]
    public async Task<IActionResult> EnviarTransaccion([FromBody] TransaccionBaseDto transaccion, [FromQuery] string tipoAcceso)
    {
        if (transaccion == null)
        {
            return BadRequest("La transacción no puede estar vacía.");
        }

        var resultado = await _mediator.Send(new SignalRRequest { MandamosTransaccion = transaccion, TipoAcceso = tipoAcceso });
        return Ok(resultado);
    }
}

