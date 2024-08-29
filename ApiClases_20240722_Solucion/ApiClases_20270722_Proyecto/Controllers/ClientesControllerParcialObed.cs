using ApiClases_20270722_Proyecto.Modelos.Clientes;

namespace ApiClases_20270722_Proyecto.Controllers
{
    public partial class ClientesController
    {
        public void simularContactos()
        {
            var numClientes = _mapper.Map<IEnumerable<ClienteBaseDto>>(_clienteRepositorio.Obtener()).Count();
            Random random = new Random();
            for (var i = 0; i < numClientes; i++)
            {
                _contactoRepositorio.Agregar(new Contacto
                {
                    Id = 0,
                    ClienteDestinoId = 0,
                });
            }
            _contactoRepositorio.Agregar()
        }
    }
}
