namespace ApiClases_20270722_Proyecto.Modelos.Clientes
{
    public class ClientePutDto : ClientePostDto
    {
        [Required]
        public int Id { get; set; }
    }
}
