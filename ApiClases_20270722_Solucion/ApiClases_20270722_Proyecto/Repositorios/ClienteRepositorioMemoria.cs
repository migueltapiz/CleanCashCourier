namespace ApiClases_20270722_Proyecto.Repositorios;

public class ClienteRepositorioMemoria
{
    public List<Cliente> Clientes { get; set; }
    public static ClienteRepositorioMemoria Instancia { get; } = new ClienteRepositorioMemoria();

    public ClienteRepositorioMemoria()
    {
        //Agregar paises a la lista
        Clientes = new List<Cliente>() {
           new Cliente(){ Id = 1, Nombre = "Ana", Apellidos = "Juanes", Usuario = "ana777", Pais = "Reino Unido" },
            new Cliente(){ Id = 2, Nombre = "Pedro", Apellidos = "Sanchez", Usuario = "elcoletas", Pais = "Estados Unidos" },
            new Cliente(){ Id = 3, Nombre = "Miguel", Apellidos = "Martinez", Usuario = "emanems", Pais = "España" },
            new Cliente(){ Id = 4, Nombre = "Juan", Apellidos = "Lopez", Usuario = "jujalag", Pais = "España" },
            new Cliente(){ Id = 5, Nombre = "Iñigo", Apellidos = "Vertiz", Usuario = "ibai", Pais = "Andorra" }
        };
    }
}
