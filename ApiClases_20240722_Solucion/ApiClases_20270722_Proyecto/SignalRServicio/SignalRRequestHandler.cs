
using Microsoft.AspNetCore.SignalR.Client;


namespace ApiClases_20270722_Proyecto.SignalRServicio;

public class SignalRRequestHandler : IRequestHandler<SignalRRequest, string>
{
    private readonly SignalRServicio _signalRServicio;

    public SignalRRequestHandler(SignalRServicio signalRServicio)
    {
        _signalRServicio = signalRServicio;
    }

    public async Task<string> Handle(SignalRRequest request, CancellationToken cancellationToken)
    {
        await _signalRServicio.SendMessageAsync("Aplicación de envio", request.Mensaje);
        return "Mensaje enviado correctamente.";
    }
}
