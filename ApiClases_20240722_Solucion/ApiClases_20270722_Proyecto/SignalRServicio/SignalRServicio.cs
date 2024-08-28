using Microsoft.AspNetCore.SignalR.Client;
// using ApiBasesDeDatosProyecto.Models;
// using ApiBasesDeDatosProyecto.Repository;


namespace ApiClases_20270722_Proyecto.SignalRServicio;

public class SignalRServicio
{
    private readonly HubConnection _hubConnection;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SignalRServicio(string hubUrl, IServiceScopeFactory serviceScopeFactory)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .Build();

        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartListeningAsync()
    {
        if (_hubConnection.State != HubConnectionState.Disconnected)
        {
            await _hubConnection.StopAsync();
        }

        // Configura el manejo de mensajes recibidos
        _hubConnection.On<string>("RecibirMensaje", (mensaje) =>
        {
            Console.WriteLine($"Mensaje recibido del simulador: {mensaje}");
        });

        // Inicia la conexión al hub
        await _hubConnection.StartAsync();
        Console.WriteLine("Conectado al hub de SignalR");
    }

    public async Task SendMessageAsync(string user, string message)
    {
        await _hubConnection.InvokeAsync("SendMessage", user, message);
    }
}