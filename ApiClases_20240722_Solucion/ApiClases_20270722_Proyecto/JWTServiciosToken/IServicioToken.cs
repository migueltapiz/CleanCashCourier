namespace ApiClases_20270722_Proyecto.JWTServiciosToken;
public interface IServicioToken
{
    string GenerateJwtToken(UsuarioAplicacion cliente);
}