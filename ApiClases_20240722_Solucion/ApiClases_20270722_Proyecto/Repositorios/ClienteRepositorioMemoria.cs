namespace ApiClases_20270722_Proyecto.Repositorios;

public class ClienteRepositorioMemoria
{
    public List<ClienteDto> Clientes { get; set; }
    public static ClienteRepositorioMemoria Instancia { get; } = new ClienteRepositorioMemoria();
    private static HttpClient _httpClient;
    public ClienteRepositorioMemoria() {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7107");
    }
    public async Task<List<ClienteDto>> ObtenerClientes() {
        var response = await _httpClient.GetAsync("/api/clients");

        response.EnsureSuccessStatusCode();

        var contenido = await response.Content.ReadFromJsonAsync<List<ClienteDtoGrupo1>>();
        List<ClienteDto> Clientes = new List<ClienteDto>();

        foreach (var cliente in contenido){
            Clientes.Add(
                    new ClienteDto() { 
                        Id = cliente.ClienteId,
                        Nombre  = cliente.Nombre,
                        Apellidos = cliente.Apellido,
                        Pais    = cliente.PaisId.ToString(),
                        Usuario = cliente.UserId
                    }
                );
            
        }
        return Clientes;
    }
}
