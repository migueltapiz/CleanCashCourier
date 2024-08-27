namespace ApiClases_20270722_Proyecto.Repositorios
{
    public interface IContarTransaccionesUltimos10AniosRepositorio
    {
        Task<int> ContarTransaccionesUltimos10AñosAsync();
    }
}
