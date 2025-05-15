using JoyeriaPremiun.Datos;
using JoyeriaPremiun.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//area de servcicios 

builder.Services.AddAutoMapper(typeof(Program));

//se agrega AddJsonOptions para evitar operaciones Ciclicas------------------

builder.Services.AddControllers().AddJsonOptions(Opciones =>
Opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
opciones.UseSqlServer("name=defaultconnection"));

builder.Services.Configure<PayPalSettings>(builder.Configuration.GetSection("PayPal"));

builder.Services.AddHttpClient<PaypalService>(); // registra PaypalService como servicio HttpClient
builder.Services.AddScoped<IPayPalService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var settings = provider.GetRequiredService<IOptions<PayPalSettings>>();
    return new PaypalService(httpClientFactory, settings);
});


builder.Services.AddHttpClient("PaypalClient", client =>
{
    client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");
});




var app = builder.Build();

//area de meddleware
app.MapControllers();



app.Run();
