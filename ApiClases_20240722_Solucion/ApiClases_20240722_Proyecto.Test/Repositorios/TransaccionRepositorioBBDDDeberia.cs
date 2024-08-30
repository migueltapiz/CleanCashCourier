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
        private readonly Contexto _contexto;
        private readonly TransaccionRepositorioBBDD<Transaccion> _repositorio;

        public TransaccionRepositorioBBDDDeberia()
        {
            // Configurar el contexto en memoria
            var options = new DbContextOptionsBuilder<Contexto>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _contexto = new Contexto(options);

            // Poblar la base de datos en memoria con datos de ejemplo
            _contexto.Transacciones.AddRange(new List<Transaccion>
        {
            new Transaccion
            {
                Id = 1,
                IdEnvia = 2,
                CantidadEnvia = 100.0,
                IdRecibe = 3,
                CantidadRecibe = 95.0,
                Fecha = DateTime.Now,
                MonedaOrigen = "USD",
                MonedaDestino = "EUR",
                CosteTransaccion = 5.0
            },
            new Transaccion
            {
                Id = 2,
                IdEnvia = 3,
                CantidadEnvia = 200.0,
                IdRecibe = 4,
                CantidadRecibe = 190.0,
                Fecha = DateTime.Now,
                MonedaOrigen = "GBP",
                MonedaDestino = "USD",
                CosteTransaccion = 10.0
            }
        });

            _contexto.SaveChanges();

            // Crear la instancia del repositorio
            _repositorio = new TransaccionRepositorioBBDD<Transaccion>(_contexto);
        }

        [Fact]
        public async Task GuardarCambios_DeberiaRetornarFalseCuandoNoHayCambios()
        {
            // Arrange

            // Act
            var resultado = await _repositorio.GuardarCambios();

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task GuardarCambios_DeberiaRetornarTrueCuandoHayCambios()
        {
            // Arrange
            var nuevaTransaccion = new Transaccion
            {
                Id = 1,
                IdEnvia = 2,
                CantidadEnvia = 110.0,
                IdRecibe = 3,
                CantidadRecibe = 95.0,
                Fecha = DateTime.Now,
                MonedaOrigen = "USD",
                MonedaDestino = "EUR",
                CosteTransaccion = 5.0
            };

            // Detach la entidad existente para evitar el error de rastreo.
            var entidadExistente = _contexto.Transacciones.Find(1);
            _contexto.Entry(entidadExistente).State = EntityState.Detached;

            // Act
            _repositorio.Actualizar(1, nuevaTransaccion);
            await _repositorio.GuardarCambios();

            // Assert
            var transaccionActualizada = _contexto.Transacciones.Find(1);
            Assert.Equal(110.0, transaccionActualizada.CantidadEnvia);
        }

        [Fact]
        public void Agregar_DeberiaAgregarTransaccion()
        {
            // Arrange
            var nuevaTransaccion = new Transaccion { Id = 4, IdEnvia = 4, CantidadEnvia = 300, IdRecibe = 5, CantidadRecibe = 270, Fecha = DateTime.Now, MonedaOrigen = "USD", MonedaDestino = "MXN", CosteTransaccion = 30 };

            // Act
            _repositorio.Agregar(nuevaTransaccion);
            _repositorio.GuardarCambios();

            // Assert
            var transaccion = _contexto.Transacciones.Find(4);
            Assert.NotNull(transaccion);
        }

        [Fact]
        public void Borrar_DeberiaBorrarTransaccion()
        {
            // Act
            _repositorio.Borrar(1);
            _repositorio.GuardarCambios();

            // Assert
            var transaccion = _contexto.Transacciones.Find(1);
            Assert.Null(transaccion);
        }

        [Fact]
        public async Task ObtenerTodosFiltrado_DeberiaFiltrarPorIdCliente()
        {
            // Arrange
            var filtro = new FiltroTransacciones { IdCliente = 1 };

            // Act
            var resultado = await _repositorio.ObtenerTodosFiltrado(filtro);

            // Assert
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
            Assert.All(resultado, t => Assert.InRange(t.Fecha, filtro.FechaInicio.Value, filtro.FechaFin.Value));
        }
    }
}

