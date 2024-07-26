using ApiClases_20270722_Proyecto.Repositorios;

namespace ApiClases_20270722_Proyecto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase{
    public readonly IClienteRepositorio repositorio;
    public ClientesController(IClienteRepositorio repositorio){
        this.repositorio = repositorio;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDtoGrupo1>>> Get(){
        return Ok(await repositorio.ObtenerClientes());
    }

    [HttpGet("{id}", Name = "getCliente")]
    public ActionResult<ClienteDto> Get(int id){
        var cliente = repositorio.ObtenerClienteId(id);
        return cliente == null ? NotFound(): Ok(cliente);
    }

    [HttpPost]
    public ActionResult<ClienteDto> Post(ClienteDto cliente) {
        
        var clienteNuevo = repositorio.Agregar(cliente);
        return CreatedAtRoute("getCliente",
                 new
                 {
                     id = clienteNuevo.Id
                 },
                 clienteNuevo);
    }

    [HttpPut]
    public ActionResult<ClienteDto> Put(int id, ClienteDto cliente) {
        var clienteActualizado =  repositorio.Actualizar(id,cliente);
        return clienteActualizado == null ? NotFound() : CreatedAtRoute("getCliente",
                  new
                  {
                      id = clienteActualizado.Id
                  },
                  clienteActualizado);
    }

    [HttpDelete]
    public ActionResult<ClienteDto> Delete(int id) {
        var clienteBorrado = repositorio.Borrar(id);

        return clienteBorrado == null ? NotFound() : Ok("Se ha borrado correctamente");
    }
}


