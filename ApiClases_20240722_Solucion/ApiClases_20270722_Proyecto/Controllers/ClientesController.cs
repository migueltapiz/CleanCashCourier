using ApiClases_20270722_Proyecto.Repositorios;
using System.Collections.Generic;
namespace ApiClases_20270722_Proyecto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase{
    public readonly IClienteRepositorio repositorio;
    private readonly IMapper _mapper;
    public ClientesController(IClienteRepositorio repositorio,IMapper mapper){
        this.repositorio = repositorio;
        _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get(){
        return Ok(_mapper.Map < IEnumerable < ClienteDto >>(await repositorio.ObtenerClientes()));
    }

    [HttpGet("{id}", Name = "getCliente")]
    public ActionResult<ClienteDto> Get(int id){
        var cliente = repositorio.ObtenerClienteId(id);
        var finalClienteDto = _mapper.Map<ClienteDto>(cliente);
        return finalClienteDto == null ? NotFound(): Ok(finalClienteDto);
    }

    [HttpPost]
    public ActionResult<ClienteDto> Post(ClienteDto cliente) {
        var finalClienteNuevo = _mapper.Map<Cliente>(cliente);
        var clienteNuevo = repositorio.Agregar(finalClienteNuevo);
        var createdClienteToReturn = _mapper.Map<ClienteDto>(clienteNuevo);
        return CreatedAtRoute("getCliente",
                 new
                 {
                     id = createdClienteToReturn.Id
                 },
                 createdClienteToReturn);
    }

    [HttpPut]
    public ActionResult<ClienteDto> Put(int id, ClienteDto cliente) {
        var finalClienteActualizado = _mapper.Map<Cliente>(cliente);
        var clienteActualizado =  repositorio.Actualizar(id, finalClienteActualizado);
        var createdClienteToReturn = _mapper.Map<ClienteDto>(clienteActualizado);
        return createdClienteToReturn == null ? NotFound() : CreatedAtRoute("getCliente",
                  new
                  {
                      id = createdClienteToReturn.Id
                  },
                  createdClienteToReturn);
    }

    [HttpDelete]
    public ActionResult<ClienteDto> Delete(int id) {
        var clienteBorrado = repositorio.Borrar(id);

        return clienteBorrado == null ? NotFound() : Ok("Se ha borrado correctamente");
    }
}


