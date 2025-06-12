using JoyeriaPremiun.Datos;
using JoyeriaPremiun.Entidades;
using JoyeriaPremiun.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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




var app = builder.Build();

//area de meddleware
app.MapControllers();



app.Run();
