namespace ApiClases_20270722_Proyecto.Repositorios
{
    public class VistaContactosRepositorio : IVistaContactoRepositorio<VContacto>
    {
        private readonly Contexto _contexto;
        public VistaContactosRepositorio(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<(List<VContacto> Data, int TotalCount)> GetVContactosAsync(VContactoParametrosFiltradoDto parametrosfiltro)
        {
            var query = _contexto.VContactos.AsQueryable();
            if (string.IsNullOrEmpty(parametrosfiltro.NombreUsuarioContacto))
            {
                query = query.Where(v => v.NombreUsuarioContacto == parametrosfiltro.NombreUsuarioContacto);
            }
            if (parametrosfiltro.IdCliente.HasValue)
            {
                query = query.Where(v => v.IdCliente == parametrosfiltro.IdCliente);
            }
            if (parametrosfiltro.PaisId.HasValue)
            {
                query = query.Where(v => v.PaisId == parametrosfiltro.PaisId);
            }
            var nuevaConsulta = query.Select(v => new VContacto
            {
                IdCliente = v.IdCliente,
                NombreUsuarioContacto = v.NombreUsuarioContacto,
                PaisId = v.PaisId,
            });
            var pageSize = parametrosfiltro.TamanoPagina;
            if (nuevaConsulta.Count() < 10)
            {
                pageSize = nuevaConsulta.Count();
            }
            var datos = new List<VContacto>();
            datos = await nuevaConsulta.Take(pageSize).ToListAsync();
            return (datos, 0);
        }
    }
}
