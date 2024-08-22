
namespace ApiClases_20270722_Proyecto.Repositorios;

public class PaisRepositorioBBDD<T>: IRepositorioGenerico<T> where T:Pais {

    private readonly Contexto _contexto;

    public PaisRepositorioBBDD(Contexto contexto) {
        _contexto = contexto;
    }

    public void Actualizar(int id, T dato) => throw new NotImplementedException();
    public void Agregar(T dato) => throw new NotImplementedException();
    public void Borrar(int id) => throw new NotImplementedException();

    public async Task<bool> GuardarCambios() {

        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<T>> Obtener() => await _contexto.Set<T>().ToListAsync();

       
    public T ObtenerPorId(int id) => (T)_contexto.Paises.FirstOrDefault(c => c.Id == id);
    public Task<object> ObtenerTodosFiltrado(int id_cliente, DateTime? fechaInicio, DateTime? fechaFin, double? cantidadEnviadaMin, double? cantidadEnviadaMax, double? cantidadRecibidaMin, double? cantidadRecibidaMax) => throw new NotImplementedException();
    public object ObtenerTransaccionId(int id_cliente, int id_transaccion) => throw new NotImplementedException();
    public Task<List<T>> ObtenerTodosFiltrado(FiltroTransacciones filtro) => throw new NotImplementedException();
    T IRepositorioGenerico<T>.ObtenerTransaccionId(int id_cliente, int id_transaccion) => throw new NotImplementedException();
}
