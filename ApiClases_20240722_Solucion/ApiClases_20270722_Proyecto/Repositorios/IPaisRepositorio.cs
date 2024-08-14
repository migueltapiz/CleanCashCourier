namespace ApiClases_20270722_Proyecto.Repositorios;

public interface IPaisRepositorio{

    Task<List<Pais>> ObtenerPaises();
    Pais ObtenerPaisId(int id);
    Task<bool> GuardarCambios();
}
