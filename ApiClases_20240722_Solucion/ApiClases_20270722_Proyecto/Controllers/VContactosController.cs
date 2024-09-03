using ApiClases_20270722_Proyecto.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace ApiClases_20270722_Proyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VContactosController : Controller
    {
        private readonly IVistaContactoRepositorio<VContacto> _vistaContactoRepositorio;
        private readonly IRepositorioGenerico<Cliente> _clienteRepositorio;
        public VContactosController(
            IVistaContactoRepositorio<VContacto> vistacontactorepositorio,
            IRepositorioGenerico<Cliente> clienteRepositorio)
        {
            _vistaContactoRepositorio = vistacontactorepositorio;
            _clienteRepositorio = clienteRepositorio;
        }
        [HttpPost("checkIfExists")]
        public async Task<ActionResult> CheckIfExists([FromBody] ModeloBusquedaContacto modelo)
        {
            string nombre;
            // Intentar decodificar el token JWT
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadJwtToken(modelo.tokenCliente);
                nombre = jwtToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            }
            catch (ArgumentException)
            {
                // Si no es un token JWT válido, asumir que es un nombre
                nombre = modelo.tokenCliente;
            }
            var idCliente = _clienteRepositorio.ObtenerPorNombre(nombre.ToString()).Id;
            if (await _vistaContactoRepositorio.CheckIfExistsInView(idCliente, modelo.NombreUsuarioClienteABuscar))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("getAllContacts")]
        public async Task<ActionResult<IEnumerable<VContacto>>> GetVistaContactos(VContactoParametrosFiltradoDto filtro)
        {
            var (contactos, count) = await _vistaContactoRepositorio.GetVContactosAsync(filtro);
            return contactos.ToList();
        }
        [HttpGet("getAllContactsById")]
        public async Task<ActionResult<IEnumerable<VContacto>>> GetVistaContactos(int id)
        {
            VContactoParametrosFiltradoDto filtro = new VContactoParametrosFiltradoDto
            {
                IdCliente = id,
                NumeroPaginas = 1,
                TamanoPagina = 30
            };
            var (contactos, count) = await _vistaContactoRepositorio.GetVContactosAsync(filtro);
            return contactos.ToList();
        }
        [HttpGet("{valor}", Name = "getAllContactsByToken")]

        public async Task<ActionResult<IEnumerable<VContacto>>> ObtenerVistaContactosPorToken(string valor)
        {
            string nombre;
            // Intentar decodificar el token JWT
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadJwtToken(valor);
                nombre = jwtToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            }
            catch (ArgumentException)
            {
                // Si no es un token JWT válido, asumir que es un nombre
                nombre = valor;
            }
            var idCliente = _clienteRepositorio.ObtenerPorNombre(nombre.ToString()).Id;
            VContactoParametrosFiltradoDto filtro = new VContactoParametrosFiltradoDto
            {
                IdCliente = idCliente,
                NumeroPaginas = 1,
                TamanoPagina = 30
            };
            var (contactos, count) = await _vistaContactoRepositorio.GetVContactosAsync(filtro);
            return contactos.ToList();
        }
    }
}
