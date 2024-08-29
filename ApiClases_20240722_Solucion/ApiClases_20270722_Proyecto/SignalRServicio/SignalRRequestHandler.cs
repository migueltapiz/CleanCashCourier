
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
        if (request.MandamosCliente != null)
        {
            var cliente = request.MandamosCliente;
            var clienteData = new
            {
                cliente.Nombre,
                cliente.Apellido,
                cliente.FechaNacimiento,
                cliente.Empleo,
                cliente.PaisId,
                cliente.Email
            };

            await _signalRServicio.SendMessageAsync("NuevoRegistro", Newtonsoft.Json.JsonConvert.SerializeObject(clienteData));
            request.MandamosCliente = null; // Limpiar la propiedad después de procesarla para que nos deje mandar la trnasacción después de su envio
            return "Datos del cliente enviados correctamente.";
        }

        else if (request.MandamosTransaccion != null)
        {
            var transaccion = request.MandamosTransaccion;
            var transaccionData = new
            {
                transaccion.IdEnvia,
                transaccion.CantidadEnvia,
                transaccion.IdRecibe,
                transaccion.CantidadRecibe,
                transaccion.Fecha
            };

            await _signalRServicio.SendMessageAsync("NuevaTransaccion", Newtonsoft.Json.JsonConvert.SerializeObject(transaccionData));
            request.MandamosTransaccion = null;
            return "Datos de la transacción enviados correctamente.";
        }

        else if (!string.IsNullOrEmpty(request.Mensaje))
        {
            await _signalRServicio.SendMessageAsync("AplicaciónDeEnvio", request.Mensaje);
            return "Mensaje enviado correctamente.";
        }

        return "No se proporcionó ningún dato para enviar.";
    }
}
