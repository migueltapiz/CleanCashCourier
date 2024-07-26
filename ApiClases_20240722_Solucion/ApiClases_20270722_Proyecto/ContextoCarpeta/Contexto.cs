
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
                                            new Cliente() { Id = 1, Nombre = "Ana", Apellidos = "Juanes", Usuario = "ana777", Pais = "Reino Unido" },
                                            new Cliente() { Id = 2, Nombre = "Pedro", Apellidos = "Sanchez", Usuario = "elcoletas", Pais = "Estados Unidos" },
                                            new Cliente() { Id = 3, Nombre = "Miguel", Apellidos = "Martinez", Usuario = "emanems", Pais = "España" },
                                            new Cliente() { Id = 4, Nombre = "Juan", Apellidos = "Lopez", Usuario = "jujalag", Pais = "España" },
                                            new Cliente() { Id = 5, Nombre = "Iñigo", Apellidos = "Vertiz", Usuario = "ibai", Pais = "Andorra" }
                                          );

            
            
        }
    }



