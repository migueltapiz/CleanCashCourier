namespace ApiClases_20270722_Proyecto.Repositorios;

public class ClienteRepositorioMemoria{
    public List<ClienteDto> Clientes { get; set; }
    public static ClienteRepositorioMemoria Instancia { get; } = new ClienteRepositorioMemoria();

    public ClienteRepositorioMemoria() {
        //Agregar paises a la lista
        Clientes = new List<ClienteDto>() {
           new ClienteDto(){ Id = 1, Nombre = "Ana", Apellidos = "Juanes", Usuario = "ana777", Pais = "Reino Unido" },
            new ClienteDto(){ Id = 2, Nombre = "Pedro", Apellidos = "Sanchez", Usuario = "elcoletas", Pais = "Estados Unidos" },
            new ClienteDto(){ Id = 3, Nombre = "Miguel", Apellidos = "Martinez", Usuario = "emanems", Pais = "España" },
            new ClienteDto(){ Id = 4, Nombre = "Juan", Apellidos = "Lopez", Usuario = "jujalag", Pais = "España" },
            new ClienteDto(){ Id = 5, Nombre = "Iñigo", Apellidos = "Vertiz", Usuario = "ibai", Pais = "Andorra" }
        };
    }
}
