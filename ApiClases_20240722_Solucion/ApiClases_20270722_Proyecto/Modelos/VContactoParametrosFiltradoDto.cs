namespace ApiClases_20270722_Proyecto.Modelos
{
    public class VContactoParametrosFiltradoDto
    {
        public int? IdCliente{ get; set; }
        public string? NombreUsuarioContacto { get; set; }
        public string? Pais { get; set; }
        public int NumeroPaginas { get; set; } = 1;
        public int TamanoPagina { get; set; } = 10;
    }
}
