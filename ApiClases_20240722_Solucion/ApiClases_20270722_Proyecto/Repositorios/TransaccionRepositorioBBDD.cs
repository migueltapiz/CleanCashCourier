

namespace ApiClases_20270722_Proyecto.Repositorios;

public class TransaccionRepositorioBBDD<T> : IRepositorioGenerico<T> where T : Transaccion
{
    private readonly Contexto _contexto;

    public TransaccionRepositorioBBDD(Contexto contexto) {
        _contexto = contexto;
    }
    public void Actualizar(int id, T transaccion) {
        transaccion.Id = id;
        _contexto.Transacciones.Update(transaccion);
    }


    public void Agregar(T transaccion) {
        _contexto.Transacciones.Add(transaccion);
    }

   

    public void Borrar(int id) {
        var transaccion = _contexto.Transacciones.FirstOrDefault(c => c.Id == id);
        _contexto.Transacciones.Remove(transaccion);
    }


    public async Task<bool> GuardarCambios() {

        return await _contexto.SaveChangesAsync() > 0;

    }

    public Task<List<T>> Obtener() => throw new NotImplementedException();
    public T ObtenerPorId(int id) => throw new NotImplementedException();

    public Task<List<T>> ObtenerTodosFiltrado(int id_cliente, DateTime? fechaInicio, DateTime? fechaFin, double? cantidadEnviadaMin, double? cantidadEnviadaMax, double? cantidadRecibidaMin, double? cantidadRecibidaMax) {

        var consulta = _contexto.Set<T>().Where(transaccion => transaccion.IdEnvia == id_cliente || transaccion.IdRecibe == id_cliente);

        if(fechaInicio != null)
        {
            consulta = consulta.Where(transaccion => fechaInicio <= transaccion.Fecha);
        }
        if(fechaFin != null)
        {
            consulta = consulta.Where(transaccion => transaccion.Fecha <= fechaFin);
        }
        if(cantidadEnviadaMin != null)
        {
            consulta = consulta.Where(transaccion => cantidadEnviadaMin <= transaccion.CantidadEnvia);
        }
        if(cantidadEnviadaMax != null)
        {
            consulta = consulta.Where(transaccion => cantidadEnviadaMax >= transaccion.CantidadEnvia);
        }
        if(cantidadRecibidaMin != null)
        {
            consulta = consulta.Where(transaccion => cantidadRecibidaMin <= transaccion.CantidadRecibe);
        }
        if(cantidadRecibidaMax != null)
        {
            consulta = consulta.Where(transaccion => cantidadRecibidaMax >= transaccion.CantidadRecibe);
        }

        return consulta.ToListAsync();
    }

    public Task<List<Transaccion>> ObtenerTransaccionesPorCliente(int user_id) {
        IQueryable<Transaccion> consulta = _contexto.Transacciones.Where(t => t.IdEnvia == user_id || t.IdRecibe == user_id);
        return consulta.ToListAsync();
    }

    public T ObtenerTransaccionId(int id_cliente, int id_transaccion) {
        return _contexto.Set<T>().Where(t => t.IdEnvia == id_cliente || t.IdRecibe == id_cliente).FirstOrDefault(c => c.Id == id_transaccion);
    }

   
}
