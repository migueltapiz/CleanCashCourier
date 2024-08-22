namespace ApiClases_20270722_Proyecto.Modelos
{
    public class ModeloRegistro : ClienteBaseDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contrasena { get; set; }

    }
}
