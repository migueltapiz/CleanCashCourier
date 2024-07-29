
namespace ApiClases_20270722_Proyecto.Repositorios;

public class ClienteRepositorioBBDD : IClienteRepositorio{
    private readonly Contexto contexto;

    public ClienteRepositorioBBDD(Contexto contexto) {
        this.contexto = contexto;
    }
    public Cliente Actualizar(int id, Cliente cliente) => throw new NotImplementedException();
    public Cliente Agregar(Cliente cliente) => throw new NotImplementedException();
    public Cliente Borrar(int id) => throw new NotImplementedException();
    public Cliente ObtenerClienteId(int id) => throw new NotImplementedException();
    Task<List<Cliente>> IClienteRepositorio.ObtenerClientes() => throw new NotImplementedException();
}
