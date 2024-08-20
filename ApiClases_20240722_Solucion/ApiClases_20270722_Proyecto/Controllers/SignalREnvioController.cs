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


    [HttpPost("enviar")]
    public async Task<IActionResult> EnviarMensaje([FromBody] string mensaje)
    {
        if (string.IsNullOrEmpty(mensaje))
        {
            return BadRequest("El mensaje no puede estar vacío.");
        }

        var resultado = await _mediator.Send(new SignalRRequest { Mensaje = mensaje });
        return Ok(resultado);
    }

}
