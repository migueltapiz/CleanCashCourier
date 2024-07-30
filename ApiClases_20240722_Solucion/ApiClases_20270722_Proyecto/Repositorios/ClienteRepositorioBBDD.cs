namespace ApiClases_20270722_Proyecto.Repositorios;

public class ClienteRepositorioBBDD : IClienteRepositorio {
    private readonly Contexto _contexto;

    public ClienteRepositorioBBDD(Contexto contexto) {
        _contexto = contexto;
    }
    public void Actualizar(int id, Cliente cliente) {
        _contexto.Clientes.Update(cliente);
    }
    public void Agregar(Cliente cliente) {
        _contexto.Clientes.Add(cliente);
    }
    public void Borrar(int id) {
        var cliente = _contexto.Clientes.FirstOrDefault(c => c.Id == id);
        _contexto.Clientes.Remove(cliente);
    }
    public Cliente ObtenerClienteId(int id) {
        return _contexto.Clientes.FirstOrDefault(c => c.Id == id);
    }
    public Task<List<Cliente>> ObtenerClientes() { 
        
        return _contexto.Clientes.ToListAsync();

    }

    public async Task<bool> GuardarCambios() {

        return await _contexto.SaveChangesAsync() > 0;

    }

    public async Task<List<Transaccion>> ObtenerTransaccionesPorCliente(string nombre) {
        //var consulta = _contexto.Transacciones.Select(transaccion => transaccion).GroupJoin(_contexto.Clientes,transaccion => transaccion.IdEnvia, cliente => cliente.Id);


        return await _contexto.Transacciones.ToListAsync();
    }
}
  