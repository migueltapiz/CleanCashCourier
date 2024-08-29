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

        private IList<ValidationResult> ValidarModelo(object modelo)
        {
            var context = new ValidationContext(modelo, serviceProvider: null, items: null);
            var resultados = new List<ValidationResult>();
            Validator.TryValidateObject(modelo, context, resultados, validateAllProperties: true);
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
        [InlineData("email@demasiadolargo..........................................................................................................................................................................................................................................com")] // Email demasiado largo
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
            var cliente = new Cliente 
            { 
                Nombre = "Juan", 
                Apellido = "Perez", 
                FechaNacimiento = DateTime.Now.AddDays(1), 
                PaisId = 1, 
                Email = "test@example.com" 
            };

            // Act
            var resultados = ValidarModelo(cliente);

            // Assert
            Assert.NotEmpty(resultados);  // Asegúrate de que hay errores de validación
            Assert.Contains(resultados, r => r.ErrorMessage == "La fecha de nacimiento no puede ser posterior a la fecha actual.");
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
        [InlineData("UsuarioConUnNombreExcesivamenteLargoQueDeberiaExcederElLimiteDeCaracteresDelCampoUsuario...............................MasCaracteresHastaExceder256..............................................................................................................")] // Usuario excesivamente largo, más de 256 caracteres
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
