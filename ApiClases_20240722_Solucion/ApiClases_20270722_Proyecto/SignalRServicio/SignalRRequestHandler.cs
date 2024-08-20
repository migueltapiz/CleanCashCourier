
namespace ApiClases_20270722_Proyecto.SignalRServicio;

public class SignalRRequestHandler : IRequestHandler<SignalRRequest, string>
{
    private readonly IHubContext<SignalRHubNotificacion> _hubContext;

    public SignalRRequestHandler(IHubContext<SignalRHubNotificacion> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task<string> Handle(SignalRRequest request, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", request.Mensaje);
        return "Mensaje enviado";
    }

}
