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

    public Task<List<Transaccion>> ObtenerTransaccionesPorCliente(string username)
    {
        int user_id = _contexto.Clientes.Where(c => c.Usuario == username).Select(c => c.Id).FirstOrDefault();
        var consulta = _contexto.Transacciones.Where(t => t.IdEnvia == user_id || t.IdRecibe == user_id);

        return consulta.ToListAsync();
    }
        /*
         public Task<List<Transaccion>> ObtenerTransaccionesPorCliente(int user_id) {
            //var consulta = _contexto.Transacciones.Select(transaccion => transaccion).GroupJoin(_contexto.Clientes,transaccion => transaccion.IdEnvia, cliente => cliente.Id);
            IQueryable<Transaccion> consulta = _contexto.Transacciones.Where(t => t.IdEnvia == user_id || t.IdRecibe == user_id);
            return consulta.ToListAsync();


            //return await _contexto.Transacciones.ToListAsync();
        }
         */
    }
