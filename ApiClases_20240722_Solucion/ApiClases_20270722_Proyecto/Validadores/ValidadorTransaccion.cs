namespace ApiClases_20270722_Proyecto.Validadores;

public class ValidadorTransaccion{
    
    

    public bool esValido(Transaccion transaccion) {

        if(transaccion == null) {
            return false;
        }

        if(transaccion.IdEnvia == transaccion.IdRecibe) {
            return false;
        }

        return transaccion.CantidadEnvia > 0;
    }
}
