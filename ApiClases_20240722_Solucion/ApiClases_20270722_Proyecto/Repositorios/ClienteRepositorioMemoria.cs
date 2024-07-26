namespace ApiClases_20270722_Proyecto.Repositorios;

public class ClienteRepositorioMemoria: IClienteRepositorio
{
    public List<ClienteDto> Clientes { get; set; }
    public int heLeido;
    public static ClienteRepositorioMemoria Instancia { get; } = new ClienteRepositorioMemoria();
    private static HttpClient _httpClient;
    public ClienteRepositorioMemoria() {
        heLeido = 0;
        Clientes = new List<ClienteDto>();
            _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7107");
    }
    public async Task<List<ClienteDto>> ObtenerClientes() {
        if(heLeido == 0)
        {
            var response = await _httpClient.GetAsync("/api/clients");

            response.EnsureSuccessStatusCode();
            var contenido = await response.Content.ReadFromJsonAsync<List<ClienteDtoGrupo1>>();

            foreach(var cliente in contenido)
            {
                Clientes.Add(
                        new ClienteDto()
                        {
                            Id = cliente.ClienteId,
                            Nombre = cliente.Nombre,
                            Apellidos = cliente.Apellido,
                            Pais = cliente.PaisId.ToString(),
                            Usuario = cliente.UserId
                        }
                    );
            }
            heLeido++;
        }
        
        
        return Clientes;
    }

    public ClienteDto ObtenerClienteId(int id) {
        return Clientes.FirstOrDefault(cliente => cliente.Id == id);
    }

    public ClienteDto Agregar(ClienteDto cliente) {
        int maxId = Clientes.Max(cliente => cliente.Id);

        var clienteNuevo = new ClienteDto()
        {
            Id = maxId + 1,
            Nombre = cliente.Nombre,
            Apellidos = cliente.Apellidos,
            Usuario = cliente.Usuario,
            Pais = cliente.Pais,
            FechaNacimiento = cliente.FechaNacimiento
        };
        Clientes.Add(clienteNuevo);
        return clienteNuevo;
    }

    public ClienteDto Actualizar(int id, ClienteDto cliente) {

        var clienteActual = this.ObtenerClienteId(id);
        if(clienteActual == null){
            return clienteActual;
        }
        // Actualizar paisActual con los datos de pais
        // TODO: Implementar AutoMapper

        clienteActual.Id = id;
        clienteActual.Nombre = cliente.Nombre;
        clienteActual.Apellidos = cliente.Apellidos;
        clienteActual.Usuario   = cliente.Usuario;
        clienteActual.Pais = cliente.Pais;
        clienteActual.FechaNacimiento = cliente.FechaNacimiento;

        return clienteActual;
    }

    public ClienteDto Borrar(int id) { 
        var cliente = this.Clientes.FirstOrDefault(c => c.Id == id);
        if(cliente != null) { 
            this.Clientes.Remove(cliente);
        }
        return cliente; 
    }
}
