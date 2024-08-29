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
            var contactos = await _vistacontactorepositorio.GetVContactosAsync(filtro);
            return contactos.Data.ToList();
        }
    }
}
