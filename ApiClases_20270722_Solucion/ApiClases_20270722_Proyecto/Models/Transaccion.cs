namespace ApiClases_20270722_Proyecto.Models;

public class Transaccion{
    public int Id { get; set; }
    public int IdEnvia { get; set; }
    public double CantidadEnvia { get; set; }
    public int IdRecibe { get; set; }
    public double CantidadRecibe { get; set; }
    public DateTime Fecha { get; set; }
}
