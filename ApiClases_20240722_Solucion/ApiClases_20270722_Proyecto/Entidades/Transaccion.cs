
namespace ApiClases_20270722_Proyecto.Entidades;

public class Transaccion
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int IdEnvia { get; set; }

    [Required]
    public double CantidadEnvia { get; set; }

    [Required]
    public int IdRecibe { get; set; }

    [Required]
    public double CantidadRecibe { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    [Required]
    public string MonedaOrigen { get; set; }

    [Required]
    public string MonedaDestino { get; set; }

    [Required]
    public double CosteTransaccion { get; set; } //Puede ser un valor fijo o un porcentaje. Hay que hablarlo.
}
