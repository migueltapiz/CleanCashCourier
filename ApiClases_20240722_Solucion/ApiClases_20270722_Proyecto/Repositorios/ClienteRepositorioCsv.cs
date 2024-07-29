namespace ApiClases_20270722_Proyecto.Repositorios;

public class ClienteRepositorioCsv : IClienteRepositorio
{
    public List<Cliente> Clientes { get; set; }
    public ClienteRepositorioCsv() {
         Clientes = new List<Cliente>();
    }
    

    public async Task<List<Cliente>> ObtenerClientes() {
        
        using(TextReader input = File.OpenText(@"C:\datos\listaclientes.csv"))
        using(CsvReader csvReader = new CsvReader(input))
        {
            IEnumerable<dynamic> records = csvReader.GetRecords<dynamic>();



            csvReader.Configuration.TrimOptions = TrimOptions.Trim;
            csvReader.Configuration.Comment = '@'; // Default is '#'
            csvReader.Configuration.AllowComments = true;
            csvReader.Configuration.HasHeaderRecord = true;


            foreach(var record in records)
            {
                Clientes.Add(new Cliente() {
                    
                    Id = int.Parse(record.Id), 
                    Nombre = record.Nombre, 
                    Apellidos = record.Apellidos, 
                    Usuario = record.Usuario, 
                    Pais = record.Pais
                });

            }

        }
        return Clientes;
       
    }
    public Cliente ObtenerClienteId(int id) {
        return Clientes.FirstOrDefault(c => c.Id == id);
    }

    public Cliente Agregar(Cliente cliente) => throw new NotImplementedException();
    public Cliente Actualizar(int id, Cliente cliente) => throw new NotImplementedException();
    public Cliente Borrar(int id) => throw new NotImplementedException();
}
