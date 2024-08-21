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
    private readonly IServicioToken _servicioToken;
    public AccountController(
        IRepositorioGenerico<Cliente> clienteRepositorio,
        IRepositorioGenerico<Pais> paisRepositorio,
        IMapper mapper,
        IServicioToken servicioToken,
        UserManager<UsuarioAplicacion> userManager
        )
    {
        _paisRepositorio = paisRepositorio;
        _clienteRepositorio = clienteRepositorio;
        _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        _userManager = userManager;
        _servicioToken = servicioToken;
    }

    //Account/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] ModeloRegistro modelo)
    {
        Console.Write("MODELO INCOMING");
        System.Diagnostics.Debug.Write("MODELO INCOMING");
        Console.Write(modelo);

        var usuario = new UsuarioAplicacion
        {
            Nombre = modelo.Nombre,
            Apellido = modelo.Apellido,
            UserName = modelo.Email.Split('@')[0],
            Email = modelo.Email,
            FechaNacimiento = modelo.FechaNacimiento,
            Empleo = modelo.Empleo,
            NombrePais = _paisRepositorio.ObtenerPorId(modelo.PaisId).Nombre,
        };
        Console.Write("USUARIO AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        Console.Write(usuario);

        System.Diagnostics.Debug.Write("USUARIO AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        var result = await _userManager.CreateAsync(usuario, modelo.Contrasena);
        if (!result.Succeeded)
        {
            Console.Write("FALLA AL CREAR CON USER MANAGER");
            System.Diagnostics.Debug.Write("FALLA AL CREAR CON USER MANAGER");
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

        var addResult = await _userManager.AddToRoleAsync(usuario, "Cliente");
        if (!addResult.Succeeded)
        {
            Console.Write("ERROR AL REGISTRAR NUEVO ROL");
            System.Diagnostics.Debug.Write("ERROR AL REGISTRAR NUEVO ROL");

            return BadRequest("Fallo al añadir el nuevo rol.");
        }

        //Generar el token y devolverlo
        var token = _servicioToken.GenerateJwtToken(usuario);
        Console.Write(token);
        return Ok(new { Token = token });
        ////return Ok( new { Message: "Cliente registrado correctamente" });
        //return Ok(new { Message = "Cliente registrado correctamente" });

    }
}
