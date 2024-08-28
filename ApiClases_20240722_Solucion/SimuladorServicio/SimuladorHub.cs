using Microsoft.AspNetCore.SignalR;

public class SimuladorHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("RecibirMensaje", $"{user}: {message}");
    }
}
