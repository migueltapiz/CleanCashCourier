using ApiClases_20270722_Proyecto.Repositorios;

namespace ApiClases_20270722_Proyecto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDtoGrupo1>>> Get(){
        return Ok(await ClienteRepositorioMemoria.Instancia.ObtenerClientes());
    }

    [HttpGet("{id}", Name = "getCliente")]
    public ActionResult<ClienteDto> Get(int id){
        var cliente = ClienteRepositorioMemoria.Instancia.Clientes.FirstOrDefault(cliente => cliente.Id == id);
        return cliente == null ? NotFound(): Ok(cliente);
    }

    [HttpPost]
    public ActionResult<ClienteDto> Post(ClienteDto cliente) {
        int maxId = 0;
        if(ClienteRepositorioMemoria.Instancia.Clientes != null)
        {
            maxId = ClienteRepositorioMemoria.Instancia.Clientes.Max(cliente => cliente.Id);
        }
        else { 
             ClienteRepositorioMemoria.Instancia.Clientes = new List<ClienteDto>();
        }
       
        var clienteNuevo = new ClienteDto()
        {
            Id = maxId + 1,
            Nombre = cliente.Nombre,
            Apellidos = cliente.Apellidos,
            Usuario = cliente.Usuario,
            Pais = cliente.Pais
        };
        ClienteRepositorioMemoria.Instancia.Clientes.Add(clienteNuevo);

        return CreatedAtRoute("getCliente",
                 new
                 {
                     id = clienteNuevo.Id
                 },
                 clienteNuevo);
    }
}


