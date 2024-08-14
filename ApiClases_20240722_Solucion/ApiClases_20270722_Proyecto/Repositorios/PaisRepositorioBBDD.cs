namespace ApiClases_20270722_Proyecto.Repositorios;

public class PaisRepositorioBBDD : IPaisRepositorio{

    private readonly Contexto _contexto;

    public PaisRepositorioBBDD(Contexto contexto) {
        _contexto = contexto;
    }
    public async Task<bool> GuardarCambios() {

        return await _contexto.SaveChangesAsync() > 0;

    }

    public async Task<List<Pais>> ObtenerPaises() {
        return await _contexto.Paises.ToListAsync();
    }
    public Pais ObtenerPaisId(int id) => _contexto.Paises.FirstOrDefault(c => c.Id == id);
}
