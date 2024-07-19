using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Clases
{
    public class Cliente
    {
        public int Id {  get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usuario { get; set; }
        public string Pais {  get; set; }

        
        public Cliente(int id, string nombre, string apellido, string usuario, string pais)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Usuario = usuario;
            Pais = pais;
        }
    }
}
