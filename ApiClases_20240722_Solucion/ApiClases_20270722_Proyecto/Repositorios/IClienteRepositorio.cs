namespace ApiClases_20270722_Proyecto.Repositorios;

public interface IClienteRepositorio{

    Task<List<Cliente>> ObtenerTransacciones();
    Cliente ObtenerClienteId(int id);

    void Agregar(Cliente cliente);

    void Actualizar(int id,Cliente cliente);

    void Borrar(int id);
    public Task<bool> GuardarCambios();


}
