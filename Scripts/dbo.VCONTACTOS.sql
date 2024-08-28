
                    CREATE VIEW VCONTACTOS 
                    AS SELECT
                      Clientes_Origen.Id AS IdCliente
                     ,Clientes_Contacto.Usuario AS NombreContacto
                     ,Paises_Contacto.Nombre AS Pais
                    FROM dbo.Contactos
                    INNER JOIN dbo.Clientes Clientes_Origen
                      ON Contactos.ClienteOrigenId = Clientes_Origen.Id
                    INNER JOIN dbo.Clientes Clientes_Contacto
                      ON Contactos.ClienteDestinoId = Clientes_Contacto.Id
                    INNER JOIN dbo.Paises Paises_Contacto
                      ON Clientes_Contacto.PaisId = Paises_Contacto.Id;