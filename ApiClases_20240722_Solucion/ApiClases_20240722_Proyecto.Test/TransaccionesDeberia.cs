using ApiClases_20270722_Proyecto.Entidades;
using ApiClases_20270722_Proyecto.Validadores;
using System.ComponentModel.DataAnnotations;

namespace ApiClases_20240722_Proyecto.Test;

public class TransaccionesDeberia{

    private readonly ValidadorTransaccion _validadorTransaccion = new ValidadorTransaccion();

    [Fact]
    public void ComprobarTransaccionValida() {
        // Arrange
        var transaccion = new Transaccion{ 
            Id = 1,
            IdEnvia = 1,
            IdRecibe = 2,
            CantidadEnvia = 2,
            CantidadRecibe = 3,
            Fecha = DateTime.Now,
        };
        var resultadoEsperado = true;

        //Act
        bool resultado = _validadorTransaccion.esValido(transaccion);

        //Assert
        Assert.Equal(resultadoEsperado,resultado);

    }
    [Theory]
    [InlineData(null)] // Prueba con transacción nula
    public void EsValido_DeberiaRetornarFalseCuandoTransaccionEsNula(Transaccion transaccion) {
        var resultado = _validadorTransaccion.esValido(transaccion);
        Assert.False(resultado);
    }

    [Theory]
    [InlineData(1, 1)] // Prueba con IdEnvia igual a IdRecibe
    public void EsValido_DeberiaRetornarFalseCuandoIdEnviaIgualAIdRecibe(int idEnvia, int idRecibe) {
        var transaccion = new Transaccion { IdEnvia = idEnvia, IdRecibe = idRecibe };
        var resultado = _validadorTransaccion.esValido(transaccion);
        Assert.False(resultado);
    }

    [Theory]
    [InlineData(1, 2, 0)] // Prueba con CantidadEnvia igual a 0
    [InlineData(1, 2, -10)] // Prueba con CantidadEnvia negativa
    public void EsValido_DeberiaRetornarFalseCuandoCantidadEnviaEsInvalida(int idEnvia, int idRecibe, double cantidadEnvia) {
        var transaccion = new Transaccion { IdEnvia = idEnvia, IdRecibe = idRecibe, CantidadEnvia = cantidadEnvia };
        var resultado = _validadorTransaccion.esValido(transaccion);
        Assert.False(resultado);
    }
}