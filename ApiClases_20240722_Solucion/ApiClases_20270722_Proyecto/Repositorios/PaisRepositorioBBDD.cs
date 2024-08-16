using ApiClases_20270722_Proyecto.ContextoCarpeta;

namespace ApiClases_20270722_Proyecto.Repositorios;

public class PaisRepositorioBBDD<T>: IRepositorioGenerico<T> where T:Pais {

    private readonly Contexto _contexto;

    public PaisRepositorioBBDD(Contexto contexto) {
        _contexto = contexto;
    }

    public void Actualizar(int id, T dato) => throw new NotImplementedException();
    public void Agregar(T dato) => throw new NotImplementedException();
    public void Borrar(int id) => throw new NotImplementedException();

    public async Task<bool> GuardarCambios() {

        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<T>> Obtener() => await _contexto.Set<T>().ToListAsync();

    
   
    public T ObtenerPorId(int id) => (T)_contexto.Paises.FirstOrDefault(c => c.Id == id);
}
