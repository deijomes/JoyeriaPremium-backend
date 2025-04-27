using JoyeriaPremiun.Datos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//area de servcicios 

builder.Services.AddAutoMapper(typeof(Program));

//se agrega AddJsonOptions para evitar operaciones Ciclicas------------------

builder.Services.AddControllers().AddJsonOptions(Opciones =>
Opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
opciones.UseSqlServer("name=defaultconnection"));


var app = builder.Build();

//area de meddleware
app.MapControllers();



app.Run();
