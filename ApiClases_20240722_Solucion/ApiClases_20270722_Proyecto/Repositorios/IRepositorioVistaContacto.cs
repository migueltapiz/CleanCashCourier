namespace ApiClases_20270722_Proyecto.Repositorios
{
    public interface IRepositorioVistaContacto<VContacto>
    {
        Task<(List<VContacto> Data, int TotalCount)> GetVContactosAsync(VContactoParametrosFiltradoDto parametrosfiltro);
    }
}
