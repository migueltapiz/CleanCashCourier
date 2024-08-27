using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiClases_20270722_Proyecto.Migrations
{
    public partial class added_contacts
    {
        public void CrearVistas(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    CREATE VIEW VCONTACTOS
                    AS SELECT
                      Clientes.Id AS IdCliente
                     ,Clientes_1.Usuario AS NombreContacto
                     ,Clientes_1.PaisId
                    FROM dbo.Contactos
                    INNER JOIN dbo.Clientes
                      ON Contactos.ClienteOrigenId = Clientes.Id
                    INNER JOIN dbo.Clientes Clientes_1
                      ON Contactos.ClienteDestinoId = Clientes_1.Id;
                ");
        }
        public void EliminarVistas(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS VCONTACTOS;");
        }
    }
}
