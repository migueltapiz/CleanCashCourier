
using ApiClases_20270722_Proyecto.Entidades;

namespace ApiClases_20270722_Proyecto.Repositorios
{
    public class VContactosRepositorioBBDD<T> : IRepositorioGenerico<T> where T : VContacto
    {
        private readonly Contexto _contexto;
        public VContactosRepositorioBBDD(Contexto contexto)
        {
            _contexto = contexto;
        }
        public void Actualizar(int id, T vcontacto)
        {
            vcontacto.IdCliente = id;
            _contexto.VContactos.Update(vcontacto);
        }

        public void Agregar(T vcontacto)
        {
            _contexto.VContactos.Add(vcontacto);
        }

        public void Borrar(int id)
        {
            var contacto = _contexto.VContactos.FirstOrDefault(c => c.IdCliente == id);
            _contexto.VContactos.Remove(contacto);
        }

        public async Task<bool> GuardarCambios()
        {
            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<List<T>> Obtener() => await _contexto.Set<T>().ToListAsync();

        public T ObtenerPorId(int id) => _contexto.Set<T>().FirstOrDefault(c => c.IdCliente == id);

        public T ObtenerPorNombre(string username) => _contexto.Set<T>().FirstOrDefault(c => c.NombreContacto == username);

        public Task<List<T>> ObtenerTodosFiltrado(FiltroTransacciones filtro)
        {
            throw new NotImplementedException();
        }

        public T ObtenerTransaccionId(int id_cliente, int id_transaccion)
        {
            throw new NotImplementedException();
        }
    }
}
