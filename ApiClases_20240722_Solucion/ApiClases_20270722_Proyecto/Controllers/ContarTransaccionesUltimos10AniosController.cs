using ApiClases_20270722_Proyecto.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiClases_20270722_Proyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContarTransaccionesUltimos10AniosController : ControllerBase
    {
        private readonly IContarTransaccionesUltimos10AniosRepositorio _repositorio;

        public ContarTransaccionesUltimos10AniosController(IContarTransaccionesUltimos10AniosRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<int>> ContarTransaccionesUltimos10AñosAsync()
        {
            var numeroTransacciones = await _repositorio.ContarTransaccionesUltimos10AñosAsync();
            return Ok(numeroTransacciones);
        }
    }
}
