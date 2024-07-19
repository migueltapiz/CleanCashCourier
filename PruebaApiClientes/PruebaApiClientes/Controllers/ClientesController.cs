using Microsoft.AspNetCore.Mvc;
using Clases;
using CsvHelper;
using static System.Runtime.InteropServices.JavaScript.JSType;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaApiClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        public IActionResult getPrueba()
        {
            var csvProcessor = new CsvFileProcessor(@"C:\datos\listaclientes.csv",string.Empty);
            List<Cliente> clientes  = csvProcessor.Process();
            return Ok(clientes);
        }
        
    }
}
