using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes
{
    public class Envio
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string UsuarioEnvia { get; set; }
        public string UsuarioRecibe { get; set; }
        public decimal Cantidad { get; set; }

        public Envio(int id, DateTime fecha, string usuarioEnvia, string usuarioRecibe, decimal cantidad)
        {
            Id = id;
            Fecha = fecha;
            UsuarioEnvia = usuarioEnvia;
            UsuarioRecibe = usuarioRecibe;
            Cantidad = cantidad;
        }

        public void EscribirCSV(string filePath)
        {
            bool fileExists = File.Exists(filePath);
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                if (!fileExists)
                {
                    sw.WriteLine("Id,Fecha,UsuarioEnvia,UsuarioRecibe,Cantidad");
                }
                sw.WriteLine($"{Id},{Fecha},{UsuarioEnvia},{UsuarioRecibe},{Cantidad}");
            }
        }
    }
}
