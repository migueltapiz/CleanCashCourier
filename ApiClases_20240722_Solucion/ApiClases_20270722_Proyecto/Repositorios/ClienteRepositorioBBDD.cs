using ApiClases_20270722_Proyecto.ContextoCarpeta;

namespace ApiClases_20270722_Proyecto.Repositorios;

public class ClienteRepositorioBBDD<T> : IRepositorioGenerico<T> where T : Cliente
{
    private readonly Contexto _contexto;

    public ClienteRepositorioBBDD(Contexto contexto) {
        _contexto = contexto;
    }
    public void Actualizar(int id, T cliente) {
        cliente.Id = id;
        _contexto.Clientes.Update(cliente);
    }
    public void Agregar(T cliente) {
        _contexto.Clientes.Add(cliente);
    }
    public void Borrar(int id) {
        var cliente = _contexto.Clientes.FirstOrDefault(c => c.Id == id);
        _contexto.Clientes.Remove(cliente);
    }


    public async Task<bool> GuardarCambios() {

        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<T>> Obtener() => await _contexto.Set<T>().Include(c => c.Pais).ToListAsync();

    public T ObtenerPorId(int id) => _contexto.Set<T>().FirstOrDefault(c => c.Id == id);

    public T ObtenerPorNombre(string nombre) => _contexto.Set<T>().FirstOrDefault(c => c.Usuario == nombre);

    public Task<List<T>> ObtenerTodosFiltrado(int id_cliente, DateTime? fechaInicio, DateTime? fechaFin, double? cantidadEnviadaMin, double? cantidadEnviadaMax, double? cantidadRecibidaMin, double? cantidadRecibidaMax) => throw new NotImplementedException();
    public T ObtenerTransaccionId(int id_cliente, int id_transaccion) => throw new NotImplementedException();
}
