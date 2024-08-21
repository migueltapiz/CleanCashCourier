
namespace ApiClases_20270722_Proyecto.Modelos;

//TODO: Código que sobra
public class AplicacionClientes : IdentityUser
{
    // Propiedades adicionales
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool IsDeleted { get; set; }

}
