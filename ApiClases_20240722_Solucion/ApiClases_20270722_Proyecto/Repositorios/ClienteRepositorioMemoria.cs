namespace ApiClases_20270722_Proyecto.Repositorios;

public class ClienteRepositorioMemoria: IClienteRepositorio
{
    public List<Cliente> Clientes { get; set; }
    public int heLeido;
    public static ClienteRepositorioMemoria Instancia { get; } = new ClienteRepositorioMemoria();
    private static HttpClient _httpClient;
    public ClienteRepositorioMemoria() {
        heLeido = 0;
        Clientes = new List<Cliente>();
            _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7107");
    }
    public async Task<List<Cliente>> ObtenerTransacciones() {
        if(heLeido == 0)
        {
            var response = await _httpClient.GetAsync("/api/clients");

            response.EnsureSuccessStatusCode();
            var contenido = await response.Content.ReadFromJsonAsync<List<ClienteDtoGrupo1>>();

            foreach(var cliente in contenido)
            {
                Clientes.Add(
                        new Cliente()
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

    public Cliente ObtenerClienteId(int id) {
        return Clientes.FirstOrDefault(cliente => cliente.Id == id);
    }

    public void Agregar(Cliente cliente) {
        int maxId = Clientes.Max(cliente => cliente.Id);

        var clienteNuevo = new Cliente()
        {
            Id = maxId + 1,
            Nombre = cliente.Nombre,
            Apellidos = cliente.Apellidos,
            Usuario = cliente.Usuario,
            Pais = cliente.Pais,
            FechaNacimiento = cliente.FechaNacimiento
        };
        Clientes.Add(clienteNuevo);
       
    }

    public void Actualizar(int id, Cliente cliente) {

        var clienteActual = this.ObtenerClienteId(id);
        
        // Actualizar paisActual con los datos de pais
        // TODO: Implementar AutoMapper
         if( clienteActual != null )
        {
            clienteActual.Id = id;
            clienteActual.Nombre = cliente.Nombre;
            clienteActual.Apellidos = cliente.Apellidos;
            clienteActual.Usuario = cliente.Usuario;
            clienteActual.Pais = cliente.Pais;
            clienteActual.FechaNacimiento = cliente.FechaNacimiento;
        }
        

       
    }

    public void Borrar(int id) { 
        var cliente = this.Clientes.FirstOrDefault(c => c.Id == id);
        if(cliente != null) { 
            this.Clientes.Remove(cliente);
        }
       
    }

    public Task<bool> GuardarCambios() => throw new NotImplementedException();
}
