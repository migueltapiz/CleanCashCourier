using ApiClases_20270722_Proyecto.Repositorios;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiClases_20270722_Proyecto.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorized]
public class AccountController : Controller
{
    public readonly IRepositorioGenerico<Cliente> _clienteRepositorio;
    private readonly IMapper _mapper;
    private readonly UserManager<UsuarioAplicacion> _userManager;
    private readonly IRepositorioGenerico<Pais> _paisRepositorio;
    public AccountController(
        IRepositorioGenerico<Cliente> clienteRepositorio,
        IRepositorioGenerico<Pais> paisRepositorio,
        IMapper mapper,
        UserManager<UsuarioAplicacion> userManager)
    {
        _paisRepositorio = paisRepositorio;
        _clienteRepositorio = clienteRepositorio;
        _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        _userManager = userManager;
    }

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
            NombrePais = modelo.NombrePais
        };

        var result = await _userManager.CreateAsync(usuario, modelo.Contrasena);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        Console.Write(modelo.Nombre);
        Console.Write(modelo.Apellido);
        Console.Write(modelo.FechaNacimiento);
        Console.Write(modelo.Empleo);
        Console.Write(_paisRepositorio.ObtenerPorNombre(modelo.NombrePais).Id);
        Console.Write(modelo.Email.Split('@')[0]);

        _clienteRepositorio.Agregar(new Cliente
        {
            Nombre = modelo.Nombre,
            Apellido = modelo.Apellido,
            FechaNacimiento = modelo.FechaNacimiento,
            Empleo = modelo.Empleo,
            PaisId = _paisRepositorio.ObtenerPorNombre(modelo.NombrePais).Id,
            Email = modelo.Email,
            Usuario = modelo.Email.Split('@')[0],
        });

        var addResult = await _userManager.AddToRoleAsync(usuario, "Cliente");
        if (!addResult.Succeeded)
        {
            return BadRequest("Fallo al añadir el nuevo rol.");
        }

        // Generar el token y devolverlo
        //var token = _tokenService.GenerateJwtToken(user);
        //return Ok(new { Token = token });
        return Ok("Cliente registrado correctamente");
    }
}
