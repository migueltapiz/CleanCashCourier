
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

    public async Task<bool> GuardarCambios()
    {
        return await _contexto.SaveChangesAsync() > 0;
    }

    public Task<List<T>> Obtener() => throw new NotImplementedException();
    public T ObtenerPorId(int id) => throw new NotImplementedException();

    public T ObtenerPorNombre(string nombre) => throw new NotImplementedException();

    public async Task<List<T>> ObtenerTodosFiltrado(FiltroTransacciones filtro) {


        var consulta = _contexto.Set<T>().Where(transaccion => transaccion.IdEnvia == filtro.IdCliente || transaccion.IdRecibe == filtro.IdCliente);

        if (filtro.IdCliente > 0)
        {
            consulta = consulta.Where(transaccion =>
                (transaccion as Transaccion).IdEnvia == filtro.IdCliente ||
                (transaccion as Transaccion).IdRecibe == filtro.IdCliente);
        }

        if (filtro.FechaInicio.HasValue)
        {
            consulta = consulta.Where(transaccion =>
                (transaccion as Transaccion).Fecha >= filtro.FechaInicio.Value);
        }

        if (filtro.FechaFin.HasValue)
        {
            consulta = consulta.Where(transaccion =>
                (transaccion as Transaccion).Fecha <= filtro.FechaFin.Value);
        }

        if (filtro.CantidadEnviadaMin.HasValue)
        {
            consulta = consulta.Where(transaccion =>
                (transaccion as Transaccion).CantidadEnvia >= filtro.CantidadEnviadaMin.Value);
        }

        if (filtro.CantidadEnviadaMax.HasValue)
        {
            consulta = consulta.Where(transaccion =>
                (transaccion as Transaccion).CantidadEnvia <= filtro.CantidadEnviadaMax.Value);
        }

        if (filtro.CantidadRecibidaMin.HasValue)
        {
            consulta = consulta.Where(transaccion =>
                (transaccion as Transaccion).CantidadRecibe >= filtro.CantidadRecibidaMin.Value);
        }

        if (filtro.CantidadRecibidaMax.HasValue)
        {
            consulta = consulta.Where(transaccion =>
                (transaccion as Transaccion).CantidadRecibe <= filtro.CantidadRecibidaMax.Value);
        }

        return await consulta.ToListAsync();
    }

   

    public Task<List<Transaccion>> ObtenerTransaccionesPorCliente(int user_id) {
        IQueryable<Transaccion> consulta = _contexto.Transacciones.Where(t => t.IdEnvia == user_id || t.IdRecibe == user_id);
        return consulta.ToListAsync();
    }

    public T ObtenerTransaccionId(int id_cliente, int id_transaccion) {
        return _contexto.Set<T>().Where(t => t.IdEnvia == id_cliente || t.IdRecibe == id_cliente).FirstOrDefault(c => c.Id == id_transaccion);
    }
}
