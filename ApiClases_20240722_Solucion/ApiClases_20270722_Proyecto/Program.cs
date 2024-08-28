using ApiClases_20270722_Proyecto.Repositorios;

var builder = WebApplication.CreateBuilder(args);

//Agregar servicio MVC
builder.Services.AddControllers();

//Agregar servicio swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IRepositorioGenerico<Transaccion>, TransaccionRepositorioBBDD<Transaccion>>();
builder.Services.AddScoped<IRepositorioGenerico<Pais>, PaisRepositorioBBDD<Pais>>();
builder.Services.AddScoped<IRepositorioGenerico<Cliente>, ClienteRepositorioBBDD<Cliente>>();
builder.Services.AddScoped<IServicioToken, ServicioToken>();
builder.Services.AddScoped<IContarPaisesConClientes, ContarPaisesConClientesRepositorio>();
builder.Services.AddScoped<IContarTransaccionesUltimos10AniosRepositorio, ContarTransaccionesUltimos10AniosRepositorio>();
// Agregar BBDD (SQLServer)
builder.Services.AddDbContext<Contexto>(options =>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<UsuarioAplicacion, IdentityRole>()
    .AddEntityFrameworkStores<Contexto>()
    .AddDefaultTokenProviders();



// Configurar JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
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

// Middleware de autenticación
app.UseAuthentication();

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