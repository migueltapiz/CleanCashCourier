
namespace ApiClases_20270722_Proyecto.Repositorios;

public class ClienteRepositorioBBDD : IClienteRepositorio{
    private readonly Contexto contexto;

    public ClienteRepositorioBBDD(Contexto contexto) {
        this.contexto = contexto;
    }
    public ClienteDto Actualizar(int id, ClienteDto cliente) => throw new NotImplementedException();
    public ClienteDto Agregar(ClienteDto cliente) => throw new NotImplementedException();
    public ClienteDto Borrar(int id) => throw new NotImplementedException();
    public ClienteDto ObtenerClienteId(int id) => throw new NotImplementedException();
    Task<List<ClienteDto>> IClienteRepositorio.ObtenerClientes() => throw new NotImplementedException();
}
