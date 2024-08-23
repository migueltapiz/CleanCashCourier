
namespace ApiClases_20270722_Proyecto.Repositorios;

public interface IRepositorioGenerico<T>
{
    Task<List<T>> Obtener();
    T ObtenerPorId(int id);
    T ObtenerPorNombre(string nombre);

    void Agregar(T dato);

    void Actualizar(int id, T dato);

    void Borrar(int id);
    public Task<bool> GuardarCambios();
    Task<List<T>> ObtenerTodosFiltrado(FiltroTransacciones filtro);
    T ObtenerTransaccionId(int id_cliente, int id_transaccion);
}
