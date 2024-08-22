
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

    Task<List<T>> IRepositorioGenerico<T>.Obtener() => throw new NotImplementedException(); // No hay que obtener todos los usuarios de golpe y sin filtros

    public T ObtenerPorId(int id) => _contexto.Set<T>().FirstOrDefault(c => c.Id == id);

    public Task<List<T>> ObtenerTodosFiltrado(FiltroTransacciones filtro) => throw new NotImplementedException();

    public T ObtenerTransaccionId(int id_cliente, int id_transaccion) => throw new NotImplementedException();

}
