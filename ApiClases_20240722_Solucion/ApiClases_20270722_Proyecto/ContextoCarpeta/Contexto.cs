
namespace ApiClases_20270722_Proyecto.ContextoCarpeta;

public class Contexto: DbContext{
    
        public Contexto(DbContextOptions<Contexto> options) : base(options) {
        }

        // DbSet es una colección de entidades que se pueden consultar, insertar, actualizar y eliminar.
        // DbSet es una propiedad de DbContext que representa una colección de entidades en la base de datos.
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Transaccion> Transacciones{ get; set; }



        // OnConfiguring se llama cuando se crea el contexto
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // Para activar el seguimiento de consultas,
            // se debe establecer el nivel de seguimiento en Debug
            // en el archivo de configuración de la aplicación (appsettings.json)
            // y se debe agregar el siguiente código 
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            // Agregar tres paises por defecto a la base de datos
            modelBuilder.Entity<Cliente>().HasData(
                                            new Cliente() { Id = 1, Nombre = "Ana", Apellidos = "Juanes", Usuario = "ana777", Pais = "Reino Unido",FechaNacimiento = new DateTime(1900,1,1) },
                                            new Cliente() { Id = 2, Nombre = "Pedro", Apellidos = "Sanchez", Usuario = "elcoletas", Pais = "Estados Unidos", FechaNacimiento = new DateTime(1900, 1, 1) },
                                            new Cliente() { Id = 3, Nombre = "Miguel", Apellidos = "Martinez", Usuario = "emanems", Pais = "España", FechaNacimiento = new DateTime(1900, 1, 1) },
                                            new Cliente() { Id = 4, Nombre = "Juan", Apellidos = "Lopez", Usuario = "jujalag", Pais = "España", FechaNacimiento = new DateTime(1900, 1, 1) },
                                            new Cliente() { Id = 5, Nombre = "Iñigo", Apellidos = "Vertiz", Usuario = "ibai", Pais = "Andorra", FechaNacimiento = new DateTime(1900, 1, 1) }
                                          );
            modelBuilder.Entity<Transaccion>().HasData(
                        new Transaccion() { Id = 1, IdEnvia = 1, CantidadEnvia = 50.5, IdRecibe = 2, CantidadRecibe = 30.2, Fecha = new DateTime(2022, 03, 15, 14, 30, 0) },
                        new Transaccion() { Id = 2, IdEnvia = 2, CantidadEnvia = 70.3, IdRecibe = 3, CantidadRecibe = 45.8, Fecha = new DateTime(2023, 08, 20, 15, 10, 0) },
                        new Transaccion() { Id = 3, IdEnvia = 3, CantidadEnvia = 25.1, IdRecibe = 4, CantidadRecibe = 15.7, Fecha = new DateTime(2024, 05, 05, 16, 20, 0) },
                        new Transaccion() { Id = 4, IdEnvia = 4, CantidadEnvia = 40.9, IdRecibe = 5, CantidadRecibe = 20.3, Fecha = new DateTime(2025, 11, 10, 17, 45, 0) },
                        new Transaccion() { Id = 5, IdEnvia = 5, CantidadEnvia = 60.2, IdRecibe = 1, CantidadRecibe = 35.6, Fecha = new DateTime(2026, 04, 25, 18, 55, 0) },
                        new Transaccion() { Id = 6, IdEnvia = 3, CantidadEnvia = 72.1, IdRecibe = 2, CantidadRecibe = 18.7, Fecha = new DateTime(2027, 09, 05, 19, 30, 0) },
                        new Transaccion() { Id = 7, IdEnvia = 4, CantidadEnvia = 64.8, IdRecibe = 1, CantidadRecibe = 42.3, Fecha = new DateTime(2028, 02, 15, 20, 15, 0) },
                        new Transaccion() { Id = 8, IdEnvia = 5, CantidadEnvia = 91.5, IdRecibe = 4, CantidadRecibe = 10.2, Fecha = new DateTime(2029, 07, 30, 21, 05, 0) },
                        new Transaccion() { Id = 9, IdEnvia = 2, CantidadEnvia = 38.6, IdRecibe = 5, CantidadRecibe = 12.4, Fecha = new DateTime(2030, 01, 10, 22, 20, 0) },
                        new Transaccion() { Id = 10, IdEnvia = 1, CantidadEnvia = 55.3, IdRecibe = 3, CantidadRecibe = 28.9, Fecha = new DateTime(2031, 06, 20, 23, 40, 0) },
                        new Transaccion() { Id = 11, IdEnvia = 4, CantidadEnvia = 80.7, IdRecibe = 2, CantidadRecibe = 15.2, Fecha = new DateTime(2032, 12, 05, 00, 10, 0) }


                                     );


    }
    }



