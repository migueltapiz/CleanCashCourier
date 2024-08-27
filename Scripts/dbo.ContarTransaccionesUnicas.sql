CREATE PROCEDURE dbo.ContarTransaccionesUnicas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS NumeroTransaccionesUnicas
    FROM (
        SELECT 
            CASE 
                WHEN IdEnvia < IdRecibe THEN IdEnvia
                ELSE IdRecibe 
            END AS ClienteA,
            CASE 
                WHEN IdEnvia > IdRecibe THEN IdEnvia
                ELSE IdRecibe 
            END AS ClienteB
        FROM Transacciones
        GROUP BY 
            CASE 
                WHEN IdEnvia < IdRecibe THEN IdEnvia
                ELSE IdRecibe 
            END,
            CASE 
                WHEN IdEnvia > IdRecibe THEN IdEnvia
                ELSE IdRecibe 
            END
    ) AS TransaccionesUnicas;
END