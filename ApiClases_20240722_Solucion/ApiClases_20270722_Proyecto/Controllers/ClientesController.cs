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
    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get() {
            return Ok(_mapper.Map<IEnumerable<ClienteDto>>(await repositorio.Obtener()));
    }


    [HttpGet("{id}", Name = "getCliente")]
    public ActionResult<ClienteDto> Get(int id) {
        var cliente = repositorio.ObtenerPorId(id);
        var finalClienteDto = _mapper.Map<ClienteDto>(cliente);
        return finalClienteDto == null ? NotFound() : Ok(finalClienteDto);
    }

    //TODO: Cuando se implemente el CLienteDtoBase, cambiaremos el Dto con el que trabajamos
    [HttpPost]
    public async Task<ActionResult<ClienteDto>> PostAsync(ClienteDto cliente)
    {
        var finalClienteNuevo = _mapper.Map<ClienteDto, Cliente>(cliente);
        repositorio.Agregar(finalClienteNuevo);

        if (await repositorio.GuardarCambios())
        {
            // Suponiendo que el Id del cliente nuevo es finalClienteNuevo.Id
            return CreatedAtAction(nameof(PostAsync), new { id = finalClienteNuevo.Id }, "Cliente añadido correctamente");
        }
        else
        {
            return BadRequest();
        }
    }


    [HttpPut]
    public async Task<ActionResult<ClienteDto>> PutAsync(int id, ClienteDto cliente)
    {
        var finalClienteActualizado = _mapper.Map<Cliente>(cliente);
        repositorio.Actualizar(id, finalClienteActualizado);

        // Devuelve un NoContent si la actualización fue exitosa
        return await repositorio.GuardarCambios() ? NoContent() : BadRequest();
    }


    [HttpDelete]
    public async Task<ActionResult<ClienteDto>> DeleteAsync(int id) {
        repositorio.Borrar(id);

        // Devuelve un NoContent si la eliminación fue exitosa
        return await repositorio.GuardarCambios() ? NoContent() : BadRequest();
    }
}
