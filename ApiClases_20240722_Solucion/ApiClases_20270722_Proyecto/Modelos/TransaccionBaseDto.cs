namespace ApiClases_20270722_Proyecto.Modelos;

public class TransaccionBaseDto: IValidatableObject
{ // TransaccionesBaseDto. Quitar ID pero guardarlo. Generar distintos Dto que hereden de este.
    //public int Id { get; set; }

    [Required]
    public int IdEnvia { get; set; }

    [Required]
    [RequiredGreaterThanZero]
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

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {

        if(IdEnvia == IdRecibe) {
            yield return new ValidationResult("El usuario que envía y que recibe no peuden ser el mismo", new[] { "Usuarios" });
        }

    }
}
