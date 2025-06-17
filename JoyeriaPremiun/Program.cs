using JoyeriaPremiun.Datos;
using JoyeriaPremiun.Entidades;
using JoyeriaPremiun.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//area de servcicios 

builder.Services.AddAutoMapper(typeof(Program));

//se agrega AddJsonOptions para evitar operaciones Ciclicas------------------

builder.Services.AddControllers().AddJsonOptions(Opciones =>
Opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
opciones.UseSqlServer("name=defaultconnection"));





builder.Services.AddCors( opciones =>
{
    opciones.AddDefaultPolicy(opcionesCors =>
    {
        opcionesCors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});



builder.Services.AddIdentityCore<Usuario>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<Usuario>>();
builder.Services.AddScoped<SignInManager<Usuario>>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication().AddJwtBearer(opciones =>
{
    opciones.MapInboundClaims = false;

    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["llaveJwt"]!)),
        ClockSkew = TimeSpan.Zero,
    };
});


builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
});


builder.Services.AddAuthorization(opciones =>
{
    opciones.AddPolicy("esAdmin", politica => politica.RequireClaim("esAdmin"));

});


builder.Services.Configure<PayPalSettings>(builder.Configuration.GetSection("PayPal"));

builder.Services.AddHttpClient<PaypalService>(); // registra PaypalService como servicio HttpClient
builder.Services.AddScoped<IPayPalService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var settings = provider.GetRequiredService<IOptions<PayPalSettings>>();
    var Context = provider.GetRequiredService<ApplicationDbContext>();
    return new PaypalService(httpClientFactory, settings, Context);
});


builder.Services.AddHttpClient("PaypalClient", client =>
{
    client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");
});


builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "JoyeriaPremium API",
        Description = "Este es un servicio web API desarrollado  para gestionar operaciones de venta y  catálogo en línea ",


        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "deibermndoza@gmail.com",
            Name = "Mndoza",
            Url = new Uri("https://github.com/deijomes")
        },

    });

    opciones.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en el campo. Ejemplo: Bearer {token}"
    });

    opciones.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
});

});

builder.Services.AddTransient<IAlmacenadorArchivos, almacenadorDeArchivos>();





var app = builder.Build();

//area de meddleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.MapControllers();



app.Run();
