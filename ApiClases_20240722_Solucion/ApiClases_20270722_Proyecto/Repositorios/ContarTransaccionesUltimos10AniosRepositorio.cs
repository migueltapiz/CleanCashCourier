
namespace ApiClases_20270722_Proyecto.Repositorios
{
    public class ContarTransaccionesUltimos10AniosRepositorio : IContarTransaccionesUltimos10AniosRepositorio
    {
        private readonly Contexto _contexto;

        public ContarTransaccionesUltimos10AniosRepositorio(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> ContarTransaccionesUltimos10AñosAsync()
        {
            var result = await _contexto.Set<ConteoResult>()
                .FromSqlRaw("EXEC ContarTransaccionesUltimos10Años")
                .ToListAsync();
            var aux = result?.FirstOrDefault();
            return aux?.Conteo ?? 0;
        }
    }
}
