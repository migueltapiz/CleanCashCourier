
namespace ApiClases_20270722_Proyecto.Repositorios;

public class TransaccionRepositorioMemoria<T>:IRepositorioGenerico<T> where T:Transaccion{

    public List<TransaccionBaseDto> Transacciones { get; set; }
    public static TransaccionRepositorioMemoria<T> Instancia { get; } = new TransaccionRepositorioMemoria<T>();

    public TransaccionRepositorioMemoria(){
        //Agregar paises a la lista
        Transacciones = new List<TransaccionBaseDto>() {
            new TransaccionBaseDto(){ IdEnvia = 101, CantidadEnvia = 50.5, IdRecibe = 201, CantidadRecibe = 30.2, Fecha = DateTime.Now, MonedaOrigen = "USD", MonedaDestino = "EUR", CosteTransaccion = 5.0 },
            new TransaccionBaseDto(){ IdEnvia = 102, CantidadEnvia = 70.3, IdRecibe = 202, CantidadRecibe = 45.8, Fecha = DateTime.Now, MonedaOrigen = "USD", MonedaDestino = "EUR", CosteTransaccion = 7.0 },
            new TransaccionBaseDto(){ IdEnvia = 103, CantidadEnvia = 25.1, IdRecibe = 203, CantidadRecibe = 15.7, Fecha = DateTime.Now, MonedaOrigen = "USD", MonedaDestino = "EUR", CosteTransaccion = 2.5 },
            new TransaccionBaseDto(){ IdEnvia = 104, CantidadEnvia = 40.9, IdRecibe = 204, CantidadRecibe = 20.3, Fecha = DateTime.Now, MonedaOrigen = "USD", MonedaDestino = "EUR", CosteTransaccion = 4.0 },
            new TransaccionBaseDto(){ IdEnvia = 105, CantidadEnvia = 60.2, IdRecibe = 205, CantidadRecibe = 35.6, Fecha = DateTime.Now, MonedaOrigen = "USD", MonedaDestino = "EUR", CosteTransaccion = 6.0 }

        };
    }

    public void Agregar(Transaccion transaccion) => throw new NotImplementedException();
    public void Actualizar(int id, Transaccion transaccion) => throw new NotImplementedException();
    public void Borrar(int id) => throw new NotImplementedException();
    public Task<bool> GuardarCambios() => throw new NotImplementedException();
    public Task<List<Transaccion>> ObtenerTodosFiltrado(DateTime? fechaInicio, DateTime? fechaFin, double? cantidadEnviadaMin, double? cantidadEnviadaMax, double? cantidadRecibidaMin, double? cantidadRecibidaMax) => throw new NotImplementedException();
    public Task<List<Transaccion>> ObtenerTransaccionesPorCliente(int user_id) => throw new NotImplementedException();
    public Task<List<Transaccion>> ObtenerTodosFiltrado(int id_cliente, DateTime? fechaInicio, DateTime? fechaFin, double? cantidadEnviadaMin, double? cantidadEnviadaMax, double? cantidadRecibidaMin, double? cantidadRecibidaMax) => throw new NotImplementedException();
    public Transaccion ObtenerTransaccionId(int id_cliente, int id_transaccion) => throw new NotImplementedException();
    public Task<List<T>> Obtener() => throw new NotImplementedException();
    public T ObtenerPorId(int id) => throw new NotImplementedException();
    public void Agregar(T dato) => throw new NotImplementedException();
    public void Actualizar(int id, T dato) => throw new NotImplementedException();
    public Task<List<T>> ObtenerTodosFiltrado(FiltroTransacciones filtro) => throw new NotImplementedException(); 
    T IRepositorioGenerico<T>.ObtenerTransaccionId(int id_cliente, int id_transaccion) => throw new NotImplementedException();

    public T ObtenerPorNombre(string nombre)
    {
        throw new NotImplementedException();
    }

    public Task<int> ContarPaisesConClientesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> ContarTransaccionesUltimos10AñosAsync()
    {
        throw new NotImplementedException();
    }
}
