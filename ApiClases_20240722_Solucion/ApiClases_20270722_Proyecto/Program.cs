
var builder = WebApplication.CreateBuilder(args);


//Agregar servicio MVC
builder.Services.AddControllers();


//Agregar servicio swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositorioGenerico<Transaccion>, TransaccionRepositorioBBDD<Transaccion>>();
builder.Services.AddScoped<IRepositorioGenerico<Pais>, PaisRepositorioBBDD<Pais>>();
builder.Services.AddScoped<IRepositorioGenerico<Cliente>, ClienteRepositorioBBDD<Cliente>>();
builder.Services.AddScoped<IRepositorioGenerico<Contacto>, ContactosRepositorioBBDD<Contacto>>();
builder.Services.AddScoped<IServicioToken, ServicioToken>();
builder.Services.AddScoped<IContarPaisesConClientes, ContarPaisesConClientesRepositorio>();
builder.Services.AddScoped<IContarTransaccionesUltimos10AniosRepositorio, ContarTransaccionesUltimos10AniosRepositorio>();
builder.Services.AddScoped<IVistaContactoRepositorio<VContacto>, VistaContactosRepositorio>();


// Agregar BBDD (SQLServer)
builder.Services.AddDbContext<Contexto>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Configurar Identity
builder.Services.AddIdentity<UsuarioAplicacion, IdentityRole>()
    .AddEntityFrameworkStores<Contexto>()
    .AddDefaultTokenProviders();


// Configurar MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


// Registra SignalRServicio
builder.Services.AddSingleton<SignalRServicio>(provider =>
{
    var serviceScopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
    var hubUrl = "https://localhost:7040/SimuladorHub"; // Reemplaza con la URL de tu hub de SignalR
    return new SignalRServicio(hubUrl, serviceScopeFactory);
});


// Registra IRequestHandler
builder.Services.AddTransient<IRequestHandler<SignalRRequest, string>, SignalRRequestHandler>();


// Configurar SignalR
builder.Services.AddSignalR();



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


//A�adir Autommaper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Configuraci�n CORS
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



//Agregar servicios a la aplicaci�n
var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.MapHub<SignalRHubNotificacion>("/signalrhubnotificacion");


// Iniciar el cliente SignalR
var signalRServicio = app.Services.GetRequiredService<SignalRServicio>();
await signalRServicio.StartListeningAsync();


await CreateRoles(app);





//Comprobar si el entorno es de desarrollo

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Redirecci�n a https
app.UseHttpsRedirection();

// Middleware de autenticaci�n
app.UseAuthentication();

//Middleweare de autorizaci�n 
app.UseAuthorization();

//Middleweare de enrutamiento --> determina que acci�n y controlador se utilizara en funci�n de la url solicitada
app.MapControllers();


// Start listening to the SignalR hub
var signalRServicios = app.Services.GetServices<SignalRServicio>();
var tareaEscucha = signalRServicios.Select(service => service.StartListeningAsync());
await Task.WhenAll(tareaEscucha);

// Runeamos la aplicacion
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
