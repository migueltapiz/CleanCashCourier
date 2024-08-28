namespace ApiClases_20270722_Proyecto.Repositorios
{
    public class RepositorioVistaContactos : IRepositorioVistaContacto<VContacto>
    {
        private readonly Contexto _contexto;
        public RepositorioVistaContactos(Contexto contexto)
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
            if (string.IsNullOrEmpty(parametrosfiltro.NombreUsuarioCliente))
            {
                query = query.Where(v => v.NombreUsuarioCliente == parametrosfiltro.NombreUsuarioCliente);
            }
            if (parametrosfiltro.PaisId.HasValue)
            {
                query = query.Where(v => v.PaisId == parametrosfiltro.PaisId);
            }
            var nuevaConsulta = query.Select(v => new VContacto
            {
                NombreUsuarioCliente = v.NombreUsuarioCliente,
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
