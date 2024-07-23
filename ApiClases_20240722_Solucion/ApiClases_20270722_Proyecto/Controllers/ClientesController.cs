﻿using ApiClases_20270722_Proyecto.Repositorios;

namespace ApiClases_20270722_Proyecto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase{
    [HttpGet]
    public async Task<IActionResult> Get(){
        return Ok(await ClienteRepositorioMemoria.Instancia.ObtenerClientes());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id){
        var cliente = ClienteRepositorioMemoria.Instancia.Clientes.FirstOrDefault(cliente => cliente.Id == id);
        return cliente == null ? NotFound(): Ok(cliente);
    }
}
