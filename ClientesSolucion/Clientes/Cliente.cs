using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Clientes
{
    public enum PaisEnum
    {
        España,
        ReinoUnido,
        EstadosUnidos
    }
    internal class Cliente
    {
        private static int nextId = 1;
        private int id;
        private string nombre;
        private string apellido;
        private string usuario;
        private PaisEnum pais;
        
        public Cliente(string nombre, string apellido, string usuario, PaisEnum pais)
            :this(nextId++, nombre, apellido, usuario, pais) { }
        public Cliente( int id, string nombre, string apellido, string usuario, PaisEnum pais)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Usuario = usuario;
            Pais = pais;
        }

        public int Id { 
            get { return id;}
            private set { id = value; }
        }
        public string Nombre { 
            get {  return nombre;} 
            set { nombre = value; }
        }
        public string Apellido
        {
            get { return apellido;}
            set { apellido = value; }
        }
        public string Usuario
        {
            get { return usuario;}
            set { usuario = value; }
        }
        public PaisEnum Pais
        {
            get { return pais;}
            set { pais = value; }
        }
    }
}
