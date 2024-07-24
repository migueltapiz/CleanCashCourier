
using ApiClases_20270722_Proyecto.Entidades;

namespace ApiClases_20270722_Proyecto.Repositorios;

public class ClienteRepositorioCsv : IClienteRepositorio
{
    public async Task<List<ClienteDto>> ObtenerClientes() {
        List<ClienteDto> Clientes = new List<ClienteDto>();
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
}
