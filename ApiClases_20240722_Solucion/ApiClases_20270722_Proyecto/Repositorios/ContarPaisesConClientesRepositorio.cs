
namespace ApiClases_20270722_Proyecto.Repositorios
{
    public class ContarPaisesConClientesRepositorio : IContarPaisesConClientes
    {
        private readonly Contexto _contexto;

        public ContarPaisesConClientesRepositorio(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<int> ContarPaisesConClientesAsync()
        {
            var result = await _contexto.Set<ConteoResult>()
                .FromSqlRaw("EXEC ContarPaisesConClientes")
                .ToListAsync();
            var aux = result?.FirstOrDefault();
            return aux?.Conteo ?? 0;
        }
    }
}
