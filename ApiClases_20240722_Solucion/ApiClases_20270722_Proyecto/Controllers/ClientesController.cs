using ApiClases_20270722_Proyecto.Repositorios;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using static ApiClases_20270722_Proyecto.Modelos.ModeloInicioSesion;
namespace ApiClases_20270722_Proyecto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    public readonly IRepositorioGenerico<Cliente> _clienteRepositorio;
    private readonly IRepositorioGenerico<Pais> _paisRepositorio;
    private readonly IServicioToken _servicioToken;
    private readonly IMapper _mapper;
    private readonly UserManager<UsuarioAplicacion> _userManager;
    private readonly SignInManager<UsuarioAplicacion> _signInManager;
    public ClientesController(
        IRepositorioGenerico<Cliente> clienteRepositorio, 
        IRepositorioGenerico<Pais> paisRepositorio,
        IServicioToken servicioToken,
        IMapper mapper,
        UserManager<UsuarioAplicacion> userManager,
        SignInManager<UsuarioAplicacion> signInManager)
    {
        _clienteRepositorio = clienteRepositorio;
        _paisRepositorio = paisRepositorio;
        _servicioToken = servicioToken;
        _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));
        _userManager = userManager;
        _signInManager = signInManager;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get() {
       return Ok(_mapper.Map<IEnumerable<ClienteDto>>(await _clienteRepositorio.Obtener()));
   }


    [HttpGet("{id}", Name = "getCliente")]
    public ActionResult<ClienteDto> Get(int id) {
        var cliente = _clienteRepositorio.ObtenerPorId(id);
        var finalClienteDto = _mapper.Map<ClienteDto>(cliente);
        return finalClienteDto == null ? NotFound() : Ok(finalClienteDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] ModeloInicioSesion modelo)
    {

        System.Diagnostics.Debug.Write("--------------------MODELO INICIO SESION--------------------");
        System.Diagnostics.Debug.Write(modelo);
        var result = await _signInManager.PasswordSignInAsync(
            modelo.Usuario,
            modelo.Contrasena,
            modelo.Recuerdame,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(modelo.Usuario);
            var token = _servicioToken.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
//TODO: Cuando se implemente el CLienteDtoBase, cambiaremos el Dto con el que trabajamos
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] ModeloRegistro modelo)
    {
        var usuario = new UsuarioAplicacion
        {
            Nombre = modelo.Nombre,
            Apellido = modelo.Apellido,
            UserName = modelo.Email.Split('@')[0],
            Email = modelo.Email,
            FechaNacimiento = modelo.FechaNacimiento,
            Empleo = modelo.Empleo,
            //NombrePais = _paisRepositorio.ObtenerPorId(modelo.PaisId).Nombre,
            PaisId = modelo.PaisId
        };
        var result = await _userManager.CreateAsync(usuario, modelo.Contrasena);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        _clienteRepositorio.Agregar(new Cliente
        {
            Nombre = modelo.Nombre,
            Apellido = modelo.Apellido,
            FechaNacimiento = modelo.FechaNacimiento,
            Empleo = modelo.Empleo,
            PaisId = modelo.PaisId,
            Email = modelo.Email,
            Usuario = modelo.Email.Split('@')[0],
        });
        await _clienteRepositorio.GuardarCambios();
        //var addClienteResult = await _clienteRepositorio.GuardarCambios();
        //System.Diagnostics.Debug.WriteLine(addClienteResult);

        var addResult = await _userManager.AddToRoleAsync(usuario, "Cliente");
        if (!addResult.Succeeded){
            return BadRequest(new { Message = "Fallo al añadir el nuevo rol." });
        }
        return Ok();
    }


    [HttpPut]
    public async Task<ActionResult<ClienteDto>> PutAsync(int id, ClienteDto cliente)
    {
        var finalClienteActualizado = _mapper.Map<Cliente>(cliente);
        _clienteRepositorio.Actualizar(id, finalClienteActualizado);

        return await repositorio.GuardarCambios() ? NoContent() : BadRequest();
    }


    [HttpDelete]
    public async Task<ActionResult<ClienteDto>> DeleteAsync(int id) {
        _clienteRepositorio.Borrar(id);
        return await _clienteRepositorio.GuardarCambios() ? NoContent() : BadRequest();
    }
    }
}
