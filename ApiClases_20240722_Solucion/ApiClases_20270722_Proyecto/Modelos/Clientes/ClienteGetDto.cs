namespace ApiClases_20270722_Proyecto.Modelos.Clientes
{
    public class ClienteGetDto : ClientePostDto
    {
        [Required]
        public int Id { get; set; }
    }
}
