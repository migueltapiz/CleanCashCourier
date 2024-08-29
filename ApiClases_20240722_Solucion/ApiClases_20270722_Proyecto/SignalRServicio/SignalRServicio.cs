using Microsoft.AspNetCore.SignalR.Client;


namespace ApiClases_20270722_Proyecto.SignalRServicio;

public class SignalRServicio
{
    private readonly HubConnection _hubConnection;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private bool _isEventRegistered = false;


    /* LO QUE TIENEN CLIENTES EN SU SIMULADOR DE LO NUESTRO:
     * 
    private readonly ILogger<MonitoringBackgroundService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly int _intervalInSeconds; 
    
    public MonitoringBackgroundService(

            ILogger<MonitoringBackgroundService> logger,
            IServiceScopeFactory serviceScopeFactory,
            int intervalInSeconds = 5) // Puedes cambiar este valor según tu necesidad
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _intervalInSeconds = intervalInSeconds;
    }*/


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

        if (!_isEventRegistered) // Queremos que el mensaje solo llegue 1 vez y sin repeticiones. Antes lo mandaba 2 veces.
        {
            // Configura el manejo de mensajes recibidos
            _hubConnection.On<string>("RecibirMensaje", (mensaje) =>
            {
                Console.WriteLine($"GestionTransacciones: {mensaje}");
            });
            _isEventRegistered = true;
        }

        // Inicia la conexión al hub
        await _hubConnection.StartAsync();
        Console.WriteLine("Conectado al hub de SignalR");
    }

    public async Task SendMessageAsync(string user, string message)
    {
        await _hubConnection.InvokeAsync("SendMessage", user, message);
    }
}
