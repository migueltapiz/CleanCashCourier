namespace ApiClases_20270722_Proyecto.Repositorios;

public class ClienteRepositorioCsv : IClienteRepositorio
{
    public List<ClienteDto> Clientes { get; set; }
    public ClienteRepositorioCsv() {
         Clientes = new List<ClienteDto>();
    }
    

    public async Task<List<ClienteDto>> ObtenerClientes() {
        
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
                Clientes.Add(new ClienteDto() {
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
    public ClienteDto ObtenerClienteId(int id) {
        return Clientes.FirstOrDefault(c => c.Id == id);
    }

    public ClienteDto Agregar(ClienteDto cliente) => throw new NotImplementedException();
    public ClienteDto Actualizar(int id, ClienteDto cliente) => throw new NotImplementedException();
    public ClienteDto Borrar(int id) => throw new NotImplementedException();
}
