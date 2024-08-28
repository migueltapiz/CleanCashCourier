using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiClases_20270722_Proyecto.Entidades;


namespace ApiClases_20240722_Proyecto.Test
{
    public class PaisDeberia
    {
        private ValidationContext ObtenerContextoValidacion(object objeto)
        {
            return new ValidationContext(objeto, serviceProvider: null, items: null);
        }

        private IList<ValidationResult> ValidarModelo(object modelo)
        {
            var resultados = new List<ValidationResult>();
            Validator.TryValidateObject(modelo, ObtenerContextoValidacion(modelo), resultados, true);
            return resultados;
        }

        [Theory]
        [InlineData(null)] // Nombre nulo
        [InlineData("")]   // Nombre vacío
        [InlineData("A very long name that exceeds the maximum allowed length of 100 characters.....................................................................")] // Nombre demasiado largo
        public void DeberiaRetornarErrorParaNombreInvalido(string nombre)
        {
            // Arrange
            var pais = new Pais { Nombre = nombre, Divisa = "USD", Iso3 = "USA" };

            // Act
            var resultados = ValidarModelo(pais);

            // Assert
            Assert.Contains(resultados, r => r.MemberNames.Contains(nameof(Pais.Nombre)));
        }

        [Theory]
        [InlineData(null)] // Divisa nula
        [InlineData("")]   // Divisa vacía
        [InlineData("A very long currency name that exceeds the maximum allowed length of 50 characters....")] // Divisa demasiado larga
        public void DeberiaRetornarErrorParaDivisaInvalida(string divisa)
        {
            // Arrange
            var pais = new Pais { Nombre = "Estados Unidos", Divisa = divisa, Iso3 = "USA" };

            // Act
            var resultados = ValidarModelo(pais);

            // Assert
            Assert.Contains(resultados, r => r.MemberNames.Contains(nameof(Pais.Divisa)));
        }

        [Theory]
        [InlineData(null)]  // Iso3 nulo
        [InlineData("")]    // Iso3 vacío
        [InlineData("US")]  // Iso3 demasiado corto
        [InlineData("USA1")] // Iso3 demasiado largo
        public void DeberiaRetornarErrorParaIso3Invalido(string iso3)
        {
            // Arrange
            var pais = new Pais { Nombre = "Estados Unidos", Divisa = "USD", Iso3 = iso3 };

            // Act
            var resultados = ValidarModelo(pais);

            // Assert
            Assert.Contains(resultados, r => r.MemberNames.Contains(nameof(Pais.Iso3)));
        }

        [Fact]
        public void DeberiaSerValidoCuandoPropiedadesSonCorrectas()
        {
            // Arrange
            var pais = new Pais { Nombre = "Estados Unidos", Divisa = "USD", Iso3 = "USA" };

            // Act
            var resultados = ValidarModelo(pais);

            // Assert
            Assert.Empty(resultados); // Si no hay errores, la lista de resultados debe estar vacía
        }
    }
}
