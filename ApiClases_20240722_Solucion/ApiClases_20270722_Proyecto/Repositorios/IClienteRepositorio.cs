namespace ApiClases_20270722_Proyecto.Repositorios;

public interface IClienteRepositorio{

    Task<List<ClienteDto>> ObtenerClientes();
    ClienteDto ObtenerClienteId(int id);

    ClienteDto Agregar(ClienteDto cliente);

    ClienteDto Actualizar(int id,ClienteDto cliente);

    ClienteDto Borrar(int id);

}
