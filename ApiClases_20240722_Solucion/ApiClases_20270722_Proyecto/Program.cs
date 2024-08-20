using ApiClases_20270722_Proyecto.ContextoCarpeta;
using ApiClases_20270722_Proyecto.Repositorios;

var builder = WebApplication.CreateBuilder(args);

//Agregar servicio MVC
builder.Services.AddControllers();

//Agregar servicio swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var dondeSacoDatos = "BBDD";
if(dondeSacoDatos == "memoria")
{
    //builder.Services.AddSingleton<IClienteRepositorio, ClienteRepositorioMemoria>();
}
else if(dondeSacoDatos == "csv")
{

    //builder.Services.AddSingleton<IClienteRepositorio, ClienteRepositorioCsv>();
}
else if(dondeSacoDatos == "BBDD") { 
    builder.Services.AddScoped<IRepositorioGenerico<Transaccion>, TransaccionRepositorioBBDD<Transaccion>>();
    
    builder.Services.AddScoped<IRepositorioGenerico<Pais>, PaisRepositorioBBDD<Pais>>();
    builder.Services.AddScoped<IRepositorioGenerico<Cliente>, ClienteRepositorioBBDD<Cliente>>();

}

// Agregar BBDD (SQLServer)
builder.Services.AddDbContext<Contexto>(options =>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Añadir Autommaper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



builder.Services.AddCors(options =>

    {

        options.AddPolicy("AllowAllOrigins",

        builder =>

        {

            builder.AllowAnyOrigin()

    .AllowAnyMethod()

    .AllowAnyHeader();

        });

    });



    //Agregar servicios a la aplicación
    var app = builder.Build();



app.UseCors("AllowAllOrigins");
//Comprobar si el entorno es de desarrollo

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Redirección a https
app.UseHttpsRedirection();

//Middleweare de autorización 
app.UseAuthorization();

//Middleweare de enrutamiento --> determina que acción y controlador se utilizara en función de la url solicitada
app.MapControllers();


app.Run();