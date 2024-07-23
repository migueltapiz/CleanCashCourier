var builder = WebApplication.CreateBuilder(args);

//Agregar servicio MVC
builder.Services.AddControllers();

//Agregar servicio swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Agregar servicios a la aplicación
var app = builder.Build();

//Configurar middleweare


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