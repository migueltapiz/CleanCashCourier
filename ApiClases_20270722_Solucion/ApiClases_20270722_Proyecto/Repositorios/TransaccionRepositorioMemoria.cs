using System.Collections.Generic;

namespace ApiClases_20270722_Proyecto.Repositorios;

public class TransaccionRepositorioMemoria{

    public List<Transaccion> Transacciones { get; set; }
    public static TransaccionRepositorioMemoria Instancia { get; } = new TransaccionRepositorioMemoria();

    public TransaccionRepositorioMemoria(){
        //Agregar paises a la lista
        Transacciones = new List<Transaccion>() {
            new Transaccion(){ Id = 1, IdEnvia = 101, CantidadEnvia = 50.5, IdRecibe = 201, CantidadRecibe = 30.2,Fecha = DateTime.Now },
            new Transaccion(){ Id = 2, IdEnvia = 102, CantidadEnvia = 70.3, IdRecibe = 202, CantidadRecibe = 45.8,Fecha = DateTime.Now},
            new Transaccion(){ Id = 3,  IdEnvia = 103, CantidadEnvia = 25.1, IdRecibe = 203, CantidadRecibe = 15.7,Fecha = DateTime.Now},
            new Transaccion(){ Id = 4, IdEnvia = 104, CantidadEnvia = 40.9, IdRecibe = 204, CantidadRecibe = 20.3,Fecha = DateTime.Now},
            new Transaccion(){ Id = 5, IdEnvia = 105, CantidadEnvia = 60.2, IdRecibe = 205, CantidadRecibe = 35.6,Fecha = DateTime.Now}
        };
    }
}
