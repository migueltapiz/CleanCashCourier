namespace ApiClases_20270722_Proyecto.Utilidades;

public static class Extensiones{
    public static bool ComprobarMayoriaEdad(this DateTime fechaNacimiento) {
        var años = (DateTime.Now.Year - fechaNacimiento.Year);
        años -= (DateTime.Now.Month < fechaNacimiento.Month) ? 1 : 0;
        return años < 18;
    }
}
