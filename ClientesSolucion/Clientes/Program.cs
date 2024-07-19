namespace Clientes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int seed = Guid.NewGuid().GetHashCode();

            // Crear una instancia de Random con la semilla aleatoria
            Random random = new Random(seed);

            

            Console.WriteLine("Programa de envío de dinero.\n");
            //Creación de las instancias de Cliente sin necesidad de especificar el ID de manera manual
            Cliente cliente1 = new Cliente("Juan", "Pérez", "HolaSoyJuan", PaisEnum.España);
            Cliente cliente2 = new Cliente("Ana", "García", "Ana777", PaisEnum.ReinoUnido);
            Cliente cliente3 = new Cliente("Carlos", "Rodriguez", "HuevosLargos", PaisEnum.EstadosUnidos);
            Cliente cliente4 = new Cliente("Obed", "Rodas", "PedroSanchez", PaisEnum.España);

            //Console.WriteLine(cliente1.Id + " " + cliente1.Usuario + " " + cliente1.Pais);
            //Console.WriteLine(cliente2.Id + " " + cliente2.Usuario + " " + cliente2.Pais);
            //Console.WriteLine(cliente3.Id + " " + cliente3.Usuario + " " + cliente3.Pais);
            //Console.WriteLine(cliente4.Id + " " + cliente4.Usuario + " " + cliente4.Pais);



            bool cont = true;


            List<Cliente> clientes = new List<Cliente>();
            clientes.Add(cliente1);
            clientes.Add(cliente2);
            clientes.Add(cliente3);
            clientes.Add(cliente4);
            while (cont)
            {
                Console.WriteLine("Indica el usuario que eres de los siguientes: ");
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine(cliente.Usuario + " ");
                }
                Console.WriteLine("\n");
                String userAux = Console.ReadLine();
                while (userAux.CompareTo("HolaSoyJuan") != 0 && userAux.CompareTo("Ana777") != 0 && userAux.CompareTo("HuevosLargos") != 0 && userAux.CompareTo("PedroSanchez") != 0)
                {
                    Console.WriteLine("Usuario incorrecto. Inserte un usuario correcto: ");
                    userAux = Console.ReadLine();
                }
                Console.WriteLine("Eres el usuario: " + userAux);

                Console.WriteLine("Dime al usuario que quieres enviar dinero");
                String userSend = Console.ReadLine();

                while ((userSend.CompareTo("HolaSoyJuan") != 0 && userSend.CompareTo("Ana777") != 0 && userSend.CompareTo("HuevosLargos") != 0 && userSend.CompareTo("PedroSanchez") != 0) || userAux.CompareTo(userSend) == 0)
                {
                    Console.WriteLine("El usuario es invalido");
                    userSend = Console.ReadLine();
                }
                Console.WriteLine("Dime la cantidad de dinero que quieres enviar");
                int cantidad = Int32.Parse(Console.ReadLine());
                while (cantidad <= 0)
                {
                    Console.WriteLine("La cantidad de dinero es invalida, intentelo de nuevo");
                    cantidad = Int32.Parse(Console.ReadLine());
                }
                Console.WriteLine($"Usuario origen: {userAux} - Usuario destino: {userSend} - Dinero enviado: {cantidad.ToString()}");

                Envio envio = new Envio(random.Next(), DateTime.Now, userAux, userSend, (decimal)cantidad);
                envio.EscribirCSV("listaenvios.csv");

                DateTime currentDate = DateTime.Now;

                // Formatear la fecha y hora como una cadena
                //string dateString = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

                //string filePath = "listaenvios.csv";

                //bool fileExists = File.Exists(filePath);
                //using (StreamWriter sw = new StreamWriter(filePath, true))
                //{
                //    if (!fileExists)
                //    {
                //        sw.WriteLine("Id,Fecha,UsuarioEnvia,UsuarioRecibe,Cantidad");
                //    }
                //    sw.WriteLine($"{random.Next()},{dateString},{userAux},{userSend},{cantidad.ToString()}");
                //}




            }


            Console.ReadKey();
        }
    }
}
