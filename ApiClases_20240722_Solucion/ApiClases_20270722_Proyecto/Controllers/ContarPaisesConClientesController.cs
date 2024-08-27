using ApiClases_20270722_Proyecto.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiClases_20270722_Proyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContarPaisesConClientesController : ControllerBase
    {
        private readonly IContarPaisesConClientes _repositorio;

        public ContarPaisesConClientesController(IContarPaisesConClientes repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<int>> ContarPaisesConClientesAsync()
        {
            var numeroPaises = await _repositorio.ContarPaisesConClientesAsync();
            return Ok(numeroPaises);
        }
    }
}
