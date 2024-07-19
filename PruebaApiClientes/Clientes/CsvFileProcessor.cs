using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Clases
{
    public class CsvFileProcessor
    {
        public string InputFilePath { get; }
        public string OutputFilePath { get; }

        public CsvFileProcessor(string inputFilePath, string outputFilePath)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }

        public List<Cliente> Process()
        {
            List<Cliente> clientes = new List<Cliente>();
            using (TextReader input = File.OpenText(InputFilePath))
            using (CsvReader csvReader = new CsvReader(input))
            {
                IEnumerable<dynamic> records = csvReader.GetRecords<dynamic>();

                

                csvReader.Configuration.TrimOptions = TrimOptions.Trim;
                csvReader.Configuration.Comment = '@'; // Default is '#'
                csvReader.Configuration.AllowComments = true;
                csvReader.Configuration.HasHeaderRecord = true;


                foreach (var record in records)
                {
                    clientes.Add(new Cliente(int.Parse(record.Id), record.Nombre, record.Apellidos, record.Usuario, record.Pais));
                    
                }
                
            }
            return clientes;
        }
    }
}
