namespace ApiClases_20270722_Proyecto.Modelos.Clientes
{
    public class ClienteRegistro : ClienteBaseDto
    {

        [Required]
        public string Contrasena { get; set; }

    }
}
