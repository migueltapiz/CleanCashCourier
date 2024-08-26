namespace ApiClases_20270722_Proyecto.Modelos
{
    public class VContacto
    {
        public int IdCliente { get; set; }
        public string? NombreContacto { get; set; }
        public int PaisId   { get; set; }


        /*
         SELECT
  Clientes.Id
 ,Clientes_1.Nombre AS NombreContacto
 ,Clientes_1.PaisId
FROM dbo.Contactos
INNER JOIN dbo.Clientes
  ON Contactos.Id_cliente_origen = Clientes.Id
INNER JOIN dbo.Clientes Clientes_1
  ON Contactos.Id_cliente_destino = Clientes_1.Id

         
         */
    }
}
