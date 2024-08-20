namespace ApiClases_20270722_Proyecto.SignalRServicio;

public class SignalRRequest : IRequest<string>
{
    public string Mensaje { get; set; }
}

