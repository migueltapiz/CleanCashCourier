namespace ApiClases_20270722_Proyecto.Modelos
{
    public class ModeloRegistro
    {
        [Required]
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contrasena { get; set; }
        [Required]
        //public string ConfirmarContrasena { get; set; }
        public string NombrePais { get; set; }
        public string? Empleo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        
    }
}
