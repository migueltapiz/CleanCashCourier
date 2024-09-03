
namespace ApiClases_20270722_Proyecto.SignalRServicio;

public class SignalRRequest : IRequest<string>
{
    public ClientePostDto MandamosCliente { get; set; }
    public TransaccionBaseDto MandamosTransaccion { get; set; }
    public string TipoAcceso { get; set; }
    public string Mensaje { get; set; }  

}
