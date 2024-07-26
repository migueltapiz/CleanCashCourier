
namespace ApiClases_20270722_Proyecto.Models;

public class ClienteDto: IValidatableObject{
    public int Id { get; set; }
    [MaxLength(50)]
    [MinLength(3)]
    public string? Nombre { get; set; }
    [MaxLength(50)]
    [MinLength(3)]
    public string? Apellidos { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string? Usuario { get; set; }
    public string? Pais { get; set; }
    [Required]
    public DateTime FechaNacimiento { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {

        if(Char.IsDigit(Usuario[0])) {
            yield return new ValidationResult("El usuario no puede empezar por un número", new[] { "Usuario" });
        }
        var años = (DateTime.Now.Year - FechaNacimiento.Year);
        años -= (DateTime.Now.Month < FechaNacimiento.Month) ? 1 : 0;
        if(años < 18){
            yield return new ValidationResult("No se puede registrar un menor de edad", new[] {"Edad"});
        }
        
    } 
}
