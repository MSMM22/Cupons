using Microsoft.EntityFrameworkCore;
using MSCupons.application.service;
using MSCupons.infrastructure.db;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "server=localhost;port=3306;database=cuponesdb;user=root;password=Admin123;";

// Registrar el contexto de base de datos
builder.Services.AddDbContext<CuponDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))));

// Registrar el servicio de cupones
builder.Services.AddScoped<CuponService>();

// Agregar controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Activar Swagger (para probar los endpoints)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();