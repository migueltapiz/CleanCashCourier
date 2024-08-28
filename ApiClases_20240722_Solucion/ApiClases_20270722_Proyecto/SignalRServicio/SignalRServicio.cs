using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;
using HubConnection = Microsoft.AspNetCore.SignalR.Client.HubConnection;


// Recepción de mensajes de un hub de SignalR
public class SignalRServicio
{
    private readonly HubConnection _hubConnection;

    public SignalRServicio(string hubUrl)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .Build();
    }

    public async Task StartListeningAsync()
    {
        // Configura el manejo de mensajes recibidos
        _hubConnection.On<string>("RecibirMensaje", (mensaje) =>
        {
            Console.WriteLine($"Mensaje recibido del simulador: {mensaje}");
        });

        // Inicia la conexión al hub
        // await _hubConnection.StartAsync();
        Console.WriteLine("Conectado al hub de SignalR");
    }
}