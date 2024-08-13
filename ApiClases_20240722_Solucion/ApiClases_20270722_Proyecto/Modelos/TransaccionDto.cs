namespace ApiClases_20270722_Proyecto.Modelos;

public class TransaccionDto: IValidatableObject{
    public int Id { get; set; }
    [Required]
    public int IdEnvia { get; set; }
    [Required]
    [RequiredGreaterThanZero]
    public double CantidadEnvia { get; set; }
    [Required]
    public int IdRecibe { get; set; }
    [Required]
    public double CantidadRecibe { get; set; }
    public DateTime Fecha { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {

        if(IdEnvia == IdRecibe) {
            yield return new ValidationResult("El usuario que envía y que recibe no peuden ser el mismo", new[] { "Usuarios" });
        }

    }
}
