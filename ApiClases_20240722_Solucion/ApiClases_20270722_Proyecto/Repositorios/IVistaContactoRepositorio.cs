namespace ApiClases_20270722_Proyecto.Repositorios
{
    public interface IVistaContactoRepositorio<VContacto>
    {
        Task<(List<VContacto> Data, int TotalCount)> GetVContactosAsync(VContactoParametrosFiltradoDto parametrosfiltro);
    }
}
