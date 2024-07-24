namespace ApiClases_20270722_Proyecto.Repositorios;

public interface IClienteRepositorio{

    public Task<List<ClienteDto>> ObtenerClientes();

}
