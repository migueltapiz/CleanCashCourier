using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClases_20270722_Proyecto.Modelos
{
    public class ContactoDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ClienteOrigenId { get; set; }
        [Required]
        public int ClienteDestinoId { get; set; }
    }
}
