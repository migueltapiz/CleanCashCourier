namespace ApiClases_20270722_Proyecto.Entidades;

public class FiltroTransacciones
{
    public int IdCliente { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public double? CantidadEnviadaMin { get; set; }
    public double? CantidadEnviadaMax { get; set; }
    public double? CantidadRecibidaMin { get; set; }
    public double? CantidadRecibidaMax { get; set; }
}
