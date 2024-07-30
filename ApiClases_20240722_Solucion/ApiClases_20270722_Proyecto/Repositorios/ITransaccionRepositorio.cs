namespace ApiClases_20270722_Proyecto.Repositorios;

public interface ITransaccionRepositorio {

    Transaccion ObtenerTransaccionId(int id);

    void Agregar(Transaccion transaccion);

    void Actualizar(int id, Transaccion transaccion);

    void Borrar(int id);
    public Task<bool> GuardarCambios();

    Task<List<Transaccion>> ObtenerTodosFiltrado(DateTime? fechaInicio, DateTime? fechaFin, double? cantidadEnviadaMin, double? cantidadEnviadaMax, double? cantidadRecibidaMin, double? cantidadRecibidaMax);
}
