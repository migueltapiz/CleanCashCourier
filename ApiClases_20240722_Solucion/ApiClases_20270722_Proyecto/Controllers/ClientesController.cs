using ApiClases_20270722_Proyecto.Repositorios;

namespace ApiClases_20270722_Proyecto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase{
    [HttpGet]
    public IActionResult Get(){
        return Ok(ClienteRepositorioMemoria.Instancia.Clientes);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id){
        var cliente = ClienteRepositorioMemoria.Instancia.Clientes.FirstOrDefault(cliente => cliente.Id == id);
        return cliente == null ? NotFound(): Ok(cliente);
    }
}
