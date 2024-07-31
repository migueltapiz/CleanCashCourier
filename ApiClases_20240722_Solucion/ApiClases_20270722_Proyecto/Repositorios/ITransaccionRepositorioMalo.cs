using ApiClases_20270722_Proyecto.ContextoCarpeta;

namespace ApiClases_20270722_Proyecto.Repositorios;

public interface ITransaccionRepositorioMalo {

    Transaccion ObtenerTransaccionId(int id_cliente,int id_transaccion);

    void Agregar(Transaccion transaccion);

    void Actualizar(int id, Transaccion transaccion);

    void Borrar(int id);
    public Task<bool> GuardarCambios();

    Task<List<Transaccion>> ObtenerTodosFiltrado(int id_cliente,DateTime? fechaInicio, DateTime? fechaFin, double? cantidadEnviadaMin, double? cantidadEnviadaMax, double? cantidadRecibidaMin, double? cantidadRecibidaMax);

    public Task<List<Transaccion>> ObtenerTransaccionesPorCliente(int user_id);
}
