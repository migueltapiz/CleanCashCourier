using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClases_20270722_Proyecto.Entidades;

public class Transaccion{
    [Key]
    public int Id { get; set; }
    public int IdEnvia { get; set; }
    public double CantidadEnvia { get; set; }
    public int IdRecibe { get; set; }
    public double CantidadRecibe { get; set; }
    public DateTime Fecha { get; set; }
}
