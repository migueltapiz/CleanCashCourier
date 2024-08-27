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
            if (string.IsNullOrEmpty(parametrosfiltro.username))
            {
                query = query.Where(v => v.NombreContacto == parametrosfiltro.username);
            }
            if (parametrosfiltro.PaisId.HasValue)
            {
                query = query.Where(v => v.PaisId == parametrosfiltro.PaisId);
            }
            var nuevaConsulta = query.Select(v => new VContacto
            {
                IdCliente = v.IdCliente,
                NombreContacto = v.NombreContacto,
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

            //var newQuery = query.Select(m => new VhistoricoDto
            //{
            //    Fecha = m.Fecha,

            //    MonedaOrigen = m.MonedaOrigen,

            //    MonedaDestino = m.MonedaDestino,

            //    ValorOrigen = m.ValorOrigen,

            //    ValorDestino = m.ValorDestino,

            //    BanderaDestino = Convert.ToBase64String(m.BanderaDestino),

            //    BanderaOrigen = Convert.ToBase64String(m.BanderaOrigen)

            //});

            //var pageSize = queryParameters.PageSize;
            //if (newQuery.Count() < 10)
            //{
            //    pageSize = newQuery.Count();
            //}
            //var data = new List<VhistoricoDto>();
            //switch (queryParameters.ordenarPor)
            //{
            //    case "valorOrigen":
            //        data = await newQuery
            //        .OrderBy(v => v.ValorOrigen)
            //        .Take(pageSize)
            //        .ToListAsync();
            //        break;
            //    case "valorDestino":
            //        data = await newQuery
            //        .OrderBy(v => v.ValorDestino)
            //        .Take(pageSize)
            //        .ToListAsync();
            //        break;
            //    //por defecto de ordena por fecha
            //    default:
            //        data = await newQuery
            //       .OrderBy(v => v.Fecha)
            //       .Take(pageSize)
            //       .ToListAsync();
            //        break;
            //}

            ///*var data = await newQuery
            //   .OrderBy(v => v.Fecha ) // O algún otro campo incremental
            //   .Take(pageSize)
            //   .ToListAsync();*/

            //// No se requiere contar total si solo avanzas página por página.
            //return (data.AsQueryable(), 0);
        }
    }
}
