namespace ApiClases_20270722_Proyecto.Repositorios;

public interface IRepositorioGenerico<T>
{
    Task<List<T>> Obtener();
    T ObtenerPorId(int id);

    void Agregar(T dato);

    void Actualizar(int id, T dato);

    void Borrar(int id);
    public Task<bool> GuardarCambios();
}
