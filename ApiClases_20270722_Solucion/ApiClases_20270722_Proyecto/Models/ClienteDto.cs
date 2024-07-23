

namespace ApiClases_20270722_Proyecto.Models;

public class ClienteDto{
    [Key]
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    public string? Usuario { get; set; }
    public string? Pais { get; set; }

}
