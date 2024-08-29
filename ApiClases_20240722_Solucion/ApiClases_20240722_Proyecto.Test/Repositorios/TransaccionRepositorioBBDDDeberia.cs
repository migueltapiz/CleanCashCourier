using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiClases_20270722_Proyecto.ContextoCarpeta;
using ApiClases_20270722_Proyecto.Entidades;
using ApiClases_20270722_Proyecto.Repositorios;
using Microsoft.EntityFrameworkCore;
using ApiClases_20270722_Proyecto.Modelos.Transacciones;
using Microsoft.AspNetCore.Routing;

namespace ApiClases_20240722_Proyecto.Test.Repositorios
{
    public class TransaccionRepositorioBBDDDeberia
    {
        private readonly Mock<Contexto> _mockContexto;
        private readonly Mock<DbSet<Transaccion>> _mockDbSet;
        private readonly List<Transaccion> _transacciones;
        private readonly TransaccionRepositorioBBDD<Transaccion> _repositorio;

        public TransaccionRepositorioBBDDDeberia()
        {
            _transacciones = new List<Transaccion>
            {
                new Transaccion { Id = 1, IdEnvia = 1, CantidadEnvia = 100, IdRecibe = 2, CantidadRecibe = 90, Fecha = new DateTime(2023, 1, 1), MonedaOrigen = "USD", MonedaDestino = "EUR", CosteTransaccion = 10 },
                new Transaccion { Id = 2, IdEnvia = 2, CantidadEnvia = 200, IdRecibe = 3, CantidadRecibe = 180, Fecha = new DateTime(2023, 1, 2), MonedaOrigen = "EUR", MonedaDestino = "GBP", CosteTransaccion = 20 },
                new Transaccion { Id = 3, IdEnvia = 1, CantidadEnvia = 150, IdRecibe = 3, CantidadRecibe = 135, Fecha = new DateTime(2023, 1, 3), MonedaOrigen = "USD", MonedaDestino = "JPY", CosteTransaccion = 15 }
            };

            _mockDbSet = _transacciones.CreateDbSetMock();
            _mockContexto = new Mock<Contexto>();
            _mockContexto.Setup(c => c.Transacciones).Returns(_mockDbSet.Object);

            _repositorio = new TransaccionRepositorioBBDD<Transaccion>(_mockContexto.Object);
        }

        [Fact]
        public async Task GuardarCambios_DeberiaRetornarTrueCuandoHayCambios()
        {
            // Arrange
            _mockContexto.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var resultado = await _repositorio.GuardarCambios();

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void Agregar_DeberiaAgregarTransaccion()
        {
            // Arrange
            var nuevaTransaccion = new Transaccion { Id = 4, IdEnvia = 4, CantidadEnvia = 300, IdRecibe = 5, CantidadRecibe = 270, Fecha = DateTime.Now, MonedaOrigen = "USD", MonedaDestino = "MXN", CosteTransaccion = 30 };

            // Act
            _repositorio.Agregar(nuevaTransaccion);

            // Assert
            _mockDbSet.Verify(m => m.Add(It.IsAny<Transaccion>()), Times.Once);
        }

        [Fact]
        public void Borrar_DeberiaBorrarTransaccion()
        {
            // Act
            _repositorio.Borrar(1);

            // Assert
            _mockDbSet.Verify(m => m.Remove(It.Is<Transaccion>(t => t.Id == 1)), Times.Once);
        }

        [Fact]
        public async Task ObtenerTodosFiltrado_DeberiaFiltrarPorIdCliente()
        {
            // Arrange
            var filtro = new FiltroTransacciones { IdCliente = 1 };

            // Act
            var resultado = await _repositorio.ObtenerTodosFiltrado(filtro);

            // Assert
            Assert.Equal(2, resultado.Count);
            Assert.All(resultado, t => Assert.True(t.IdEnvia == 1 || t.IdRecibe == 1));
        }

        [Fact]
        public async Task ObtenerTodosFiltrado_DeberiaFiltrarPorRangoDeFechas()
        {
            // Arrange
            var filtro = new FiltroTransacciones { FechaInicio = new DateTime(2023, 1, 2), FechaFin = new DateTime(2023, 1, 3) };

            // Act
            var resultado = await _repositorio.ObtenerTodosFiltrado(filtro);

            // Assert
            Assert.Equal(2, resultado.Count);
            Assert.All(resultado, t => Assert.InRange(t.Fecha, filtro.FechaInicio.Value, filtro.FechaFin.Value));
        }
    }
}

