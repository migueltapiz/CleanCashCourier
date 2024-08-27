CREATE PROCEDURE dbo.ContarPaisesConClientes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(DISTINCT PaisId) AS NumeroPaises
    FROM Clientes;
END