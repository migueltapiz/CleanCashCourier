using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClases_20270722_Proyecto.Entidades;

public class Cliente
{
    [Key]
    public int Id { get; set; }

    //[ForeignKey(nameof(Usuario))]
    //public int UserId { get; set; }

    [Required]
    [StringLength(25)]
    public string Nombre { get; set; }

    [Required]
    [StringLength(30)]
    public string Apellido { get; set; }

    [Required]
    public DateTime FechaNacimiento { get; set; }

    [StringLength(50)]
    public string Empleo { get; set; }

    [Required]
    public int PaisId { get; set; }

    [ForeignKey(nameof(PaisId))]
    public Pais Pais { get; set; }  // Navegación a la entidad Pais


    [Required]
    [StringLength(256)]

    public string Email { get; set; }

    [StringLength(256)]
    public string Usuario { get; set; }
}