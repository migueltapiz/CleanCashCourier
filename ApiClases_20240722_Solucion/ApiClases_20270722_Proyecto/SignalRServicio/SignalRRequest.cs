﻿namespace ApiClases_20270722_Proyecto.SignalRServicio;

public class SignalRRequest : IRequest<string>
{
    public ClienteDto MandamosCliente { get; set; }
    public TransaccionDto MandamosTransaccion { get; set; }
    public string Mensaje { get; set; }

}
