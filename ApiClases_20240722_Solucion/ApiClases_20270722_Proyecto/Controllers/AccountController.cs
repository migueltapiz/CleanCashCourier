
namespace ApiClases_20270722_Proyecto.Controllers;
  

/* public class AccountController : Controller
{
    private readonly UserManager<AplicacionClientes> _userManager;
    private readonly SignInManager<AplicacionClientes> _signInManager;
    private readonly IServicioToken _tokenService;

    public AccountController(UserManager<AplicacionClientes> userManager, SignInManager<AplicacionClientes> signInManager, IServicioToken tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }



    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {

        string rolPorDefecto = "Admin";
        DateTime FechaNac = DateTimeOffset.FromUnixTimeMilliseconds(model.FechaNacimiento).UtcDateTime;


        var user = new AplicacionClientes
        {
            FullName = model.Nombre + " " + model.Apellido,
            UserName = model.Email,
            Email = model.Email,
            DateOfBirth = FechaNac,
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        // Generar el token y devolverlo
        var token = _tokenService.GenerateJwtToken(user);
        return Ok(new { Token = token });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = _tokenService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }


} */
