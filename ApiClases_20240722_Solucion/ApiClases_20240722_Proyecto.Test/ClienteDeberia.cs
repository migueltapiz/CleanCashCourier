using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiClases_20270722_Proyecto.Entidades;


namespace ApiClases_20240722_Proyecto.Test
{
    public class ClienteDeberia
    {
        private ValidationContext ObtenerContextoValidacion(object objeto)
        {
            return new ValidationContext(objeto, serviceProvider: null, items: null);
        }

        public static List<ValidationResult> ValidarModelo(object modelo)
        {
            var resultados = new List<ValidationResult>();
            var contexto = new ValidationContext(modelo);

            Validator.TryValidateObject(modelo, contexto, resultados, true);

            return resultados;
        }

        [Theory]
        [InlineData(null)]  // Nombre nulo
        [InlineData("")]    // Nombre vacío
        [InlineData("NombreDemasiadoLargoParaElCampoQueTieneUnMaximoDe25Caracteres")] // Nombre demasiado largo
        public void DeberiaRetornarErrorParaNombreInvalido(string nombre)
        {
            // Arrange
            var cliente = new Cliente { Nombre = nombre, Apellido = "Perez", FechaNacimiento = DateTime.Now.AddYears(-30), PaisId = 1, Email = "test@example.com" };

            // Act
            var resultados = ValidarModelo(cliente);

            // Assert
            Assert.Contains(resultados, r => r.MemberNames.Contains(nameof(Cliente.Nombre)));
        }

        [Theory]
        [InlineData(null)]  // Apellido nulo
        [InlineData("")]    // Apellido vacío
        [InlineData("ApellidoDemasiadoLargoParaElCampoQueTieneUnMaximoDe30Caracteres")] // Apellido demasiado largo
        public void DeberiaRetornarErrorParaApellidoInvalido(string apellido)
        {
            // Arrange
            var cliente = new Cliente { Nombre = "Juan", Apellido = apellido, FechaNacimiento = DateTime.Now.AddYears(-30), PaisId = 1, Email = "test@example.com" };

            // Act
            var resultados = ValidarModelo(cliente);

            // Assert
            Assert.Contains(resultados, r => r.MemberNames.Contains(nameof(Cliente.Apellido)));
        }

        [Theory]
        [InlineData("EmailInvalido")]  // Email sin formato válido
        [InlineData("email@demasiadolargo....................................................................................com")] // Email demasiado largo
        public void DeberiaRetornarErrorParaEmailInvalido(string email)
        {
            // Arrange
            var cliente = new Cliente { Nombre = "Juan", Apellido = "Perez", FechaNacimiento = DateTime.Now.AddYears(-30), PaisId = 1, Email = email };

            // Act
            var resultados = ValidarModelo(cliente);

            // Assert
            Assert.NotEmpty(resultados);  // Asegurarse de que hay errores de validación
            Assert.Contains(resultados, r => r.MemberNames.Contains(nameof(Cliente.Email)));
        }


        [Fact]
        public void DeberiaRetornarErrorSiFechaNacimientoEsFutura()
        {
            // Arrange
            var cliente = new Cliente { Nombre = "Juan", Apellido = "Perez", FechaNacimiento = DateTime.Now.AddDays(1), PaisId = 1, Email = "test@example.com" };

            // Act
            var resultados = ValidarModelo(cliente);

            // Assert
            Assert.NotEmpty(resultados);  // Asegurarse de que hay errores de validación
            Assert.Contains(resultados, r => r.MemberNames.Contains(nameof(Cliente.FechaNacimiento)));
        }


        [Theory]
        [InlineData(0)]  // PaisId igual a 0
        [InlineData(-1)] // PaisId negativo
        public void DeberiaRetornarErrorParaPaisIdInvalido(int paisId)
        {
            // Arrange
            var cliente = new Cliente { Nombre = "Juan", Apellido = "Perez", FechaNacimiento = DateTime.Now.AddYears(-30), PaisId = paisId, Email = "test@example.com" };

            // Act
            var resultados = ValidarModelo(cliente);

            // Assert
            Assert.Contains(resultados, r => r.MemberNames.Contains(nameof(Cliente.PaisId)));
        }

        [Fact]
        public void DeberiaSerValidoCuandoPropiedadesSonCorrectas()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nombre = "Juan",
                Apellido = "Perez",
                FechaNacimiento = DateTime.Now.AddYears(-30),
                PaisId = 1,
                Email = "test@example.com",
                Usuario = "usuario123",
                Empleo = "Desarrollador"
            };

            // Act
            var resultados = ValidarModelo(cliente);

            // Assert
            Assert.Empty(resultados); // Si no hay errores, la lista de resultados debe estar vacía
        }

        [Theory]
        [InlineData("DesarrolladorDeSoftwareConUnNombreExcesivamenteLargoParaElCampoEmpleo")] // Empleo demasiado largo
        public void DeberiaRetornarErrorParaEmpleoInvalido(string empleo)
        {
            // Arrange
            var cliente = new Cliente
            {
                Nombre = "Juan",
                Apellido = "Perez",
                FechaNacimiento = DateTime.Now.AddYears(-30),
                PaisId = 1,
                Email = "test@example.com",
                Empleo = empleo
            };

            // Act
            var resultados = ValidarModelo(cliente);

            // Assert
            Assert.Contains(resultados, r => r.MemberNames.Contains(nameof(Cliente.Empleo)));
        }

        [Theory]
        [InlineData("UsuarioConUnNombreExcesivamenteLargoParaElCampoUsuarioQueDeberiaSerCorto")] // Usuario demasiado largo
        public void DeberiaRetornarErrorParaUsuarioInvalido(string usuario)
        {
            // Arrange
            var cliente = new Cliente
            {
                Nombre = "Juan",
                Apellido = "Perez",
                FechaNacimiento = DateTime.Now.AddYears(-30),
                PaisId = 1,
                Email = "test@example.com",
                Usuario = usuario
            };

            // Act
            var resultados = ValidarModelo(cliente);

            // Assert
            Assert.NotEmpty(resultados);  // Asegurarse de que hay errores de validación
            Assert.Contains(resultados, r => r.MemberNames.Contains(nameof(Cliente.Usuario)));
        }

    }
}
