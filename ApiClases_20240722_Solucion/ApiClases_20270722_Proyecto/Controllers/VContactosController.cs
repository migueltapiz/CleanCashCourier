using ApiClases_20270722_Proyecto.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace ApiClases_20270722_Proyecto.Controllers
{
    public class VContactosController : Controller
    {
        private readonly IVistaContactoRepositorio<VContacto> _vistacontactorepositorio;
        public VContactosController(
            IVistaContactoRepositorio<VContacto> vistacontactorepositorio,
            IMapper mapper)
        {
            _vistacontactorepositorio = vistacontactorepositorio;
        }
        [HttpPost("getAllContacts")]
        public async Task<ActionResult<IEnumerable<VContacto>>> GetVistaContactos(VContactoParametrosFiltradoDto filtro)
        {
            var (contactos, count) = await _vistacontactorepositorio.GetVContactosAsync(filtro);
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
            var (contactos, count) = await _vistacontactorepositorio.GetVContactosAsync(filtro);
            return contactos.ToList();
        }
    }
}
