namespace ApiClases_20270722_Proyecto.Entidades;

public class Cliente{
    [Key]
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    [Required]
    [StringLength(20)]
    public string? Usuario { get; set; }
    public string? Pais { get; set; }
    public DateTime FechaNacimiento { get; internal set; }
}