using ApiClases_20270722_Proyecto.ContextoCarpeta;
using System.Data;

namespace ApiClases_20270722_Proyecto.Repositorios;

public class PaisRepositorioBBDD<T>: IRepositorioGenerico<T> where T:Pais {

    private readonly Contexto _contexto;

    public PaisRepositorioBBDD(Contexto contexto) {
        _contexto = contexto;
    }

    public async Task<List<string>> ObtenerPaisesDesdeProcedimiento()
    {
        var paises = new List<string>();

        using (var connection = _contexto.Database.GetDbConnection())
        {
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "dbo.procedure1";  // Nombre del procedimiento almacenado
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        paises.Add(reader.GetString(0));  // Asumiendo que el nombre del país está en la primera columna
                    }
                }
            }
        }

        return paises;
    }
    public void Actualizar(int id, T dato) => throw new NotImplementedException();
    public void Agregar(T dato) => throw new NotImplementedException();
    public void Borrar(int id) => throw new NotImplementedException();

    public async Task<bool> GuardarCambios() {

        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<T>> Obtener() => await _contexto.Set<T>().ToListAsync();

    
   
    public T ObtenerPorId(int id) => (T)_contexto.Paises.FirstOrDefault(c => c.Id == id);
    public T ObtenerPorNombre(string nombre) => (T)_contexto.Paises.FirstOrDefault(c => c.Nombre == nombre);
    public Task<object> ObtenerTodosFiltrado(int id_cliente, DateTime? fechaInicio, DateTime? fechaFin, double? cantidadEnviadaMin, double? cantidadEnviadaMax, double? cantidadRecibidaMin, double? cantidadRecibidaMax) => throw new NotImplementedException();
    public object ObtenerTransaccionId(int id_cliente, int id_transaccion) => throw new NotImplementedException();
    Task<List<T>> IRepositorioGenerico<T>.ObtenerTodosFiltrado(int id_cliente, DateTime? fechaInicio, DateTime? fechaFin, double? cantidadEnviadaMin, double? cantidadEnviadaMax, double? cantidadRecibidaMin, double? cantidadRecibidaMax) => throw new NotImplementedException();
    T IRepositorioGenerico<T>.ObtenerTransaccionId(int id_cliente, int id_transaccion) => throw new NotImplementedException();
}
