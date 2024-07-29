namespace ApiClases_20270722_Proyecto.Repositorios;

public interface IClienteRepositorio{

    Task<List<Cliente>> ObtenerClientes();
    Cliente ObtenerClienteId(int id);

    Cliente Agregar(Cliente cliente);

    Cliente Actualizar(int id,Cliente cliente);

    Cliente Borrar(int id);

}
