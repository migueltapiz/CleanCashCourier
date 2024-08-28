
using Microsoft.AspNetCore.SignalR.Client;

namespace ApiClases_20270722_Proyecto.SignalRServicio;

public class SignalRRequestHandler : IRequestHandler<SignalRRequest, string>
{
    private readonly HubConnection _hubConnection;

    public SignalRRequestHandler(HubConnection hubConnection)
    {
        _hubConnection = hubConnection;
    }

    public async Task<string> Handle(SignalRRequest request, CancellationToken cancellationToken)
    {
        await _hubConnection.InvokeAsync("ReceiveNotification", "User", request.Mensaje);
        return "Mensaje enviado correctamente.";
    }
}
