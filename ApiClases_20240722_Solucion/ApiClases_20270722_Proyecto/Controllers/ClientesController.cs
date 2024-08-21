using ApiClases_20270722_Proyecto.Repositorios;
using System.Collections.Generic;
namespace ApiClases_20270722_Proyecto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    public readonly IRepositorioGenerico<Cliente> repositorio;
    private readonly IMapper _mapper;
    public ClientesController(IRepositorioGenerico<Cliente> repositorio, IMapper mapper) {
        this.repositorio = repositorio;
        _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteBaseDto>>> Get() {
        return Ok(_mapper.Map<IEnumerable<ClienteBaseDto>>(await repositorio.Obtener()));
    }


    //TODO: ClienteGetDto
    [HttpGet("{id}", Name = "getCliente")]
    public ActionResult<ClienteGetDto> Get(int id) {
        var cliente = repositorio.ObtenerPorId(id);
        var finalClienteDto = _mapper.Map<ClienteGetDto>(cliente);
        return finalClienteDto == null ? NotFound() : Ok(finalClienteDto);
    }

    //TODO: ClientePostDto
    [HttpPost]
    public async Task<ActionResult<ClientePostDto>> PostAsync(ClientePostDto cliente) {
        var finalClienteNuevo = _mapper.Map<ClienteBaseDto, Cliente>(cliente);
        repositorio.Agregar(finalClienteNuevo);

        return await repositorio.GuardarCambios() ? Ok("Cliente añadido correctamente") : BadRequest();

    }

    //TODO: ClientePutDto
    [HttpPut]
    public async Task<ActionResult<ClientePutDto>> PutAsync(int id, ClientePutDto cliente) {
        var finalClienteActualizado = _mapper.Map<Cliente>(cliente);
        repositorio.Actualizar(id, finalClienteActualizado);
        return await repositorio.GuardarCambios() ? Ok("Cliente actualizado correctamente") : BadRequest();
    }

    //TODO: ClienteDeleteDto
    [HttpDelete]
    public async Task<ActionResult<ClienteDeleteDto>> DeleteAsync(int id) {
        repositorio.Borrar(id);
        return await repositorio.GuardarCambios() ? Ok("Cliente borrado correctamente") : BadRequest();
    }
}
