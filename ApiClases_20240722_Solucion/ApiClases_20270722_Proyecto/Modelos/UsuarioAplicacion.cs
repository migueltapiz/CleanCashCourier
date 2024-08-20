using Microsoft.AspNetCore.Identity;

namespace ApiClases_20270722_Proyecto.Modelos
{
    public class UsuarioAplicacion : IdentityUser
    {
        public  string Nombre{ get; set; }
        public  string Apellido{ get; set; }
        public  DateTime FechaNacimiento{ get; set; }
        public  string Empleo{ get; set; }
        public  string NombrePais{ get; set; }
        //Usuario
        //Email
        //Contraseña*** estan con el identity user
    }
}
