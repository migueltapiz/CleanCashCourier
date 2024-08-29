
using ApiClases_20270722_Proyecto.SignalRServicio;


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
    public async Task<IActionResult> EnviarCliente([FromBody] ClienteDto cliente)
    {
        if (cliente == null)
        {
            return BadRequest("El cliente no puede estar vacío.");
        }

        var resultado = await _mediator.Send(new SignalRRequest { MandamosCliente = cliente });
        return Ok(resultado);
    }


    [HttpPost("enviarTransaccion")]
    public async Task<IActionResult> EnviarTransaccion([FromBody] TransaccionDto transaccion)
    {
        if (transaccion == null)
        {
            return BadRequest("La transacción no puede estar vacía.");
        }

        var resultado = await _mediator.Send(new SignalRRequest { MandamosTransaccion = transaccion });
        return Ok(resultado);
    }
}
