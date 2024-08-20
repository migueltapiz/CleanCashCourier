using ApiClases_20270722_Proyecto.ContextoCarpeta;
using ApiClases_20270722_Proyecto.Repositorios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;

var builder = WebApplication.CreateBuilder(args);

//Agregar servicio MVC
builder.Services.AddControllers();

//Agregar servicio swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//var dondeSacoDatos = "BBDD";
//if(dondeSacoDatos == "memoria")
//{
//    //builder.Services.AddSingleton<IClienteRepositorio, ClienteRepositorioMemoria>();
//}
//else if(dondeSacoDatos == "csv")
//{

//    //builder.Services.AddSingleton<IClienteRepositorio, ClienteRepositorioCsv>();
//}
//else if(dondeSacoDatos == "BBDD") { 
//    //TODO : considerar cambiarlo por scope

//}

builder.Services.AddScoped<IRepositorioGenerico<Transaccion>, TransaccionRepositorioBBDD<Transaccion>>();
builder.Services.AddScoped<IRepositorioGenerico<Pais>, PaisRepositorioBBDD<Pais>>();
builder.Services.AddScoped<IRepositorioGenerico<Cliente>, ClienteRepositorioBBDD<Cliente>>();
// Agregar BBDD (SQLServer)
builder.Services.AddDbContext<Contexto>(options =>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<UsuarioAplicacion, IdentityRole>()
    .AddEntityFrameworkStores<Contexto>()
    .AddDefaultTokenProviders();

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
await CreateRoles(app);



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


async Task CreateRoles(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Cliente", "Administrador" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}