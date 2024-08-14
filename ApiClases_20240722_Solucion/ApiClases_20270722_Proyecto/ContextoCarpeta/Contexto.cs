
namespace ApiClases_20270722_Proyecto.ContextoCarpeta;

public class Contexto: DbContext{
    
        public Contexto(DbContextOptions<Contexto> options) : base(options) {
        }

        // DbSet es una colección de entidades que se pueden consultar, insertar, actualizar y eliminar.
        // DbSet es una propiedad de DbContext que representa una colección de entidades en la base de datos.
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Transaccion> Transacciones{ get; set; }
        public DbSet<Pais> Paises { get; set; }



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
                        new Cliente() { Id = 5, Nombre = "Iñigo", Apellidos = "Vertiz", Usuario = "ibai", Pais = "Andorra", FechaNacimiento = new DateTime(1900, 1, 1) },
                        new Cliente { Id = 6, Nombre = "Barack", Apellidos = "Obama", Usuario = "baracko", Pais = "Estados Unidos", FechaNacimiento = new DateTime(1961, 8, 4) },
                        new Cliente { Id = 7, Nombre = "Lionel", Apellidos = "Messi", Usuario = "leomessi", Pais = "Argentina", FechaNacimiento = new DateTime(1987, 6, 24) },
                        new Cliente { Id = 8, Nombre = "Elon", Apellidos = "Musk", Usuario = "elonmusk", Pais = "Sudáfrica", FechaNacimiento = new DateTime(1971, 6, 28) },
                        new Cliente { Id = 9, Nombre = "Oprah", Apellidos = "Winfrey", Usuario = "oprahw", Pais = "Estados Unidos", FechaNacimiento = new DateTime(1954, 1, 29) },
                        new Cliente { Id = 10, Nombre = "Bill", Apellidos = "Gates", Usuario = "billgates", Pais = "Estados Unidos", FechaNacimiento = new DateTime(1955, 10, 28) },
                        new Cliente { Id = 11, Nombre = "Angela", Apellidos = "Merkel", Usuario = "angelam", Pais = "Alemania", FechaNacimiento = new DateTime(1954, 7, 17) },
                        new Cliente { Id = 12, Nombre = "Shakira", Apellidos = "Ripoll", Usuario = "shakira", Pais = "Colombia", FechaNacimiento = new DateTime(1977, 2, 2) },
                        new Cliente { Id = 13, Nombre = "Steve", Apellidos = "Jobs", Usuario = "stevejobs", Pais = "Estados Unidos", FechaNacimiento = new DateTime(1955, 2, 24) },
                        new Cliente { Id = 14, Nombre = "Mark", Apellidos = "Zuckerberg", Usuario = "markz", Pais = "Estados Unidos", FechaNacimiento = new DateTime(1984, 5, 14) },
                        new Cliente { Id = 15, Nombre = "Serena", Apellidos = "Williams", Usuario = "serenaw", Pais = "Estados Unidos", FechaNacimiento = new DateTime(1981, 9, 26) },
                        new Cliente { Id = 16, Nombre = "Cristiano", Apellidos = "Ronaldo", Usuario = "cr7", Pais = "Portugal", FechaNacimiento = new DateTime(1985, 2, 5) },
                        new Cliente { Id = 17, Nombre = "LeBron", Apellidos = "James", Usuario = "lebronj", Pais = "Estados Unidos", FechaNacimiento = new DateTime(1984, 12, 30) },
                        new Cliente { Id = 18, Nombre = "Elena", Apellidos = "Kagan", Usuario = "elenak", Pais = "Estados Unidos", FechaNacimiento = new DateTime(1960, 4, 28) },
                        new Cliente { Id = 19, Nombre = "J.K.", Apellidos = "Rowling", Usuario = "jkrowling", Pais = "Reino Unido", FechaNacimiento = new DateTime(1965, 7, 31) },
                        new Cliente { Id = 20, Nombre = "Dwayne", Apellidos = "Johnson", Usuario = "therock", Pais = "Estados Unidos", FechaNacimiento = new DateTime(1972, 5, 2) }
            );
            modelBuilder.Entity<Transaccion>().HasData(
                        new Transaccion { Id = 1, IdEnvia = 6, CantidadEnvia = 1000.50, IdRecibe = 7, CantidadRecibe = 1130.57, Fecha = new DateTime(2015, 3, 15, 9, 30, 0) },
                        new Transaccion { Id = 2, IdEnvia = 8, CantidadEnvia = 2000.75, IdRecibe = 9, CantidadRecibe = 2560.96, Fecha = new DateTime(2018, 6, 10, 14, 45, 0) },
                        new Transaccion { Id = 3, IdEnvia = 10, CantidadEnvia = 1500.25, IdRecibe = 11, CantidadRecibe = 1740.29, Fecha = new DateTime(2020, 8, 21, 16, 0, 0) },
                        new Transaccion { Id = 4, IdEnvia = 12, CantidadEnvia = 2500.33, IdRecibe = 13, CantidadRecibe = 3500.46, Fecha = new DateTime(2011, 11, 30, 11, 15, 0) },
                        new Transaccion { Id = 5, IdEnvia = 14, CantidadEnvia = 3000.67, IdRecibe = 15, CantidadRecibe = 4260.95, Fecha = new DateTime(2017, 2, 7, 8, 30, 0) },
                        new Transaccion { Id = 6, IdEnvia = 16, CantidadEnvia = 4000.99, IdRecibe = 17, CantidadRecibe = 2920.73, Fecha = new DateTime(2021, 12, 25, 19, 45, 0) },
                        new Transaccion { Id = 7, IdEnvia = 18, CantidadEnvia = 500.45, IdRecibe = 19, CantidadRecibe = 790.58, Fecha = new DateTime(2012, 4, 18, 7, 0, 0) },
                        new Transaccion { Id = 8, IdEnvia = 20, CantidadEnvia = 600.35, IdRecibe = 6, CantidadRecibe = 888.48, Fecha = new DateTime(2019, 7, 13, 13, 30, 0) },
                        new Transaccion { Id = 9, IdEnvia = 7, CantidadEnvia = 700.12, IdRecibe = 8, CantidadRecibe = 1085.55, Fecha = new DateTime(2014, 5, 9, 10, 15, 0) },
                        new Transaccion { Id = 10, IdEnvia = 9, CantidadEnvia = 800.75, IdRecibe = 10, CantidadRecibe = 1240.92, Fecha = new DateTime(2016, 9, 14, 15, 0, 0) },
                        new Transaccion { Id = 11, IdEnvia = 11, CantidadEnvia = 900.63, IdRecibe = 12, CantidadRecibe = 1350.36, Fecha = new DateTime(2022, 3, 12, 12, 30, 0) },
                        new Transaccion { Id = 12, IdEnvia = 13, CantidadEnvia = 100.90, IdRecibe = 14, CantidadRecibe = 140.73, Fecha = new DateTime(2023, 1, 1, 6, 45, 0) },
                        new Transaccion { Id = 13, IdEnvia = 15, CantidadEnvia = 200.33, IdRecibe = 16, CantidadRecibe = 246.40, Fecha = new DateTime(2024, 6, 17, 17, 30, 0) },
                        new Transaccion { Id = 14, IdEnvia = 17, CantidadEnvia = 300.50, IdRecibe = 18, CantidadRecibe = 402.67, Fecha = new DateTime(2013, 2, 25, 9, 0, 0) },
                        new Transaccion { Id = 15, IdEnvia = 19, CantidadEnvia = 400.80, IdRecibe = 20, CantidadRecibe = 548.34, Fecha = new DateTime(2020, 8, 11, 14, 15, 0) },
                        new Transaccion { Id = 16, IdEnvia = 6, CantidadEnvia = 5000.95, IdRecibe = 7, CantidadRecibe = 7250.44, Fecha = new DateTime(2010, 12, 31, 11, 30, 0) },
                        new Transaccion { Id = 17, IdEnvia = 8, CantidadEnvia = 3500.45, IdRecibe = 9, CantidadRecibe = 4270.87, Fecha = new DateTime(2017, 11, 5, 16, 45, 0) },
                        new Transaccion { Id = 18, IdEnvia = 10, CantidadEnvia = 6000.25, IdRecibe = 11, CantidadRecibe = 7680.35, Fecha = new DateTime(2015, 3, 24, 18, 0, 0) },
                        new Transaccion { Id = 19, IdEnvia = 12, CantidadEnvia = 4200.67, IdRecibe = 13, CantidadRecibe = 4980.12, Fecha = new DateTime(2019, 5, 22, 7, 30, 0) },
                        new Transaccion { Id = 20, IdEnvia = 14, CantidadEnvia = 4800.85, IdRecibe = 15, CantidadRecibe = 6960.40, Fecha = new DateTime(2011, 10, 10, 20, 15, 0) },
                        new Transaccion { Id = 21, IdEnvia = 16, CantidadEnvia = 5500.25, IdRecibe = 17, CantidadRecibe = 8030.50, Fecha = new DateTime(2021, 7, 18, 10, 0, 0) },
                        new Transaccion { Id = 22, IdEnvia = 18, CantidadEnvia = 3100.34, IdRecibe = 19, CantidadRecibe = 4327.15, Fecha = new DateTime(2018, 4, 2, 15, 45, 0) },
                        new Transaccion { Id = 23, IdEnvia = 20, CantidadEnvia = 2900.75, IdRecibe = 6, CantidadRecibe = 3357.86, Fecha = new DateTime(2014, 11, 19, 9, 15, 0) },
                        new Transaccion { Id = 24, IdEnvia = 7, CantidadEnvia = 1700.44, IdRecibe = 8, CantidadRecibe = 2400.63, Fecha = new DateTime(2023, 8, 30, 12, 0, 0) },
                        new Transaccion { Id = 25, IdEnvia = 9, CantidadEnvia = 650.92, IdRecibe = 10, CantidadRecibe = 995.71, Fecha = new DateTime(2012, 6, 5, 8, 30, 0) },
                        new Transaccion { Id = 26, IdEnvia = 11, CantidadEnvia = 4500.55, IdRecibe = 12, CantidadRecibe = 6120.88, Fecha = new DateTime(2020, 1, 27, 14, 0, 0) },
                        new Transaccion { Id = 27, IdEnvia = 13, CantidadEnvia = 3800.68, IdRecibe = 14, CantidadRecibe = 4346.78, Fecha = new DateTime(2022, 4, 8, 11, 15, 0) },
                        new Transaccion { Id = 28, IdEnvia = 15, CantidadEnvia = 2700.45, IdRecibe = 16, CantidadRecibe = 3159.52, Fecha = new DateTime(2013, 9, 15, 13, 0, 0) },
                        new Transaccion { Id = 29, IdEnvia = 17, CantidadEnvia = 1900.34, IdRecibe = 18, CantidadRecibe = 2652.87, Fecha = new DateTime(2016, 12, 22, 15, 45, 0) },
                        new Transaccion { Id = 30, IdEnvia = 19, CantidadEnvia = 5100.15, IdRecibe = 20, CantidadRecibe = 6120.57, Fecha = new DateTime(2024, 10, 18, 18, 0, 0) }
            );
            modelBuilder.Entity<Pais>().HasData(
                new Pais { Id = 1, Nombre = "España", Divisa = "EUR", Iso3 = "ESP" },
                new Pais { Id = 2, Nombre = "Francia", Divisa = "EUR", Iso3 = "FRA" },
                new Pais { Id = 3, Nombre = "Italia", Divisa = "EUR", Iso3 = "ITA" },
                new Pais { Id = 4, Nombre = "Reino Unido", Divisa = "GBP", Iso3 = "GBR" },
                new Pais { Id = 5, Nombre = "Estados Unidos", Divisa = "USD", Iso3 = "USA" },
                new Pais { Id = 6, Nombre = "Andorra", Divisa = "EUR", Iso3 = "AND" },
                new Pais { Id = 7, Nombre = "Argentina", Divisa = "ARS", Iso3 = "ARG" },
                new Pais { Id = 8, Nombre = "Sudáfrica", Divisa = "ZAR", Iso3 = "ZAF" },
                new Pais { Id = 9, Nombre = "Alemania", Divisa = "EUR", Iso3 = "DEU" },
                new Pais { Id = 10, Nombre = "Colombia", Divisa = "COP", Iso3 = "COL" },
                new Pais { Id = 11, Nombre = "Portugal", Divisa = "EUR", Iso3 = "PRT" }
        );


    }
    }



