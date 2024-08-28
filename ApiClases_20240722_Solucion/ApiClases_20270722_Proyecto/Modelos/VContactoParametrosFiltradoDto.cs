namespace ApiClases_20270722_Proyecto.Modelos
{
    public class VContactoParametrosFiltradoDto
    {
        public string? NombreUsuarioCliente { get; set; }
        public string? NombreUsuarioContacto { get; set; }
        public int? PaisId { get; set; }
        public int NumeroPaginas { get; set; } = 1;
        public int TamanoPagina { get; set; } = 10;
    }
}
