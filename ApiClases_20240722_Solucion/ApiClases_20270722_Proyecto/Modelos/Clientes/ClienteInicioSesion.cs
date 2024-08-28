namespace ApiClases_20270722_Proyecto.Modelos.Clientes;
public class ClienteInicioSesion
{
    [Required]
    public string Usuario { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; }

    [Display(Name = "Remember me?")]
    public bool Recuerdame { get; set; }
}
