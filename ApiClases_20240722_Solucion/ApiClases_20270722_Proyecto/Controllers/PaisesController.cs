using ApiClases_20270722_Proyecto.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiClases_20270722_Proyecto.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaisesController : ControllerBase{
    public readonly IRepositorioGenerico<Pais> repositorio;
    private readonly IMapper _mapper;

    public PaisesController(IRepositorioGenerico<Pais> repositorio, IMapper mapper) {
        this.repositorio = repositorio;
        _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaisDto>>> Get() {


        //var clientes = await repositorio.ObtenerClientes();
        //var clientesDto = _mapper.Map<IEnumerable<ClienteDto>>(clientes);

        return Ok(_mapper.Map<IEnumerable<PaisDto>>(await repositorio.Obtener()));
        //return Ok(clientesDto);
    }
    [HttpGet("{id}", Name = "getPais")]
    public ActionResult<ClienteDto> Get(int id) {
        var pais = repositorio.ObtenerPorId(id);
        var finaloaisDto = _mapper.Map<Pais>(pais);
        return finaloaisDto == null ? NotFound() : Ok(finaloaisDto);
    }
}
