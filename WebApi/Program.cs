using App.Services;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Banco para persistencia de dados
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlite("Data Source=routes.db"));

// Informações ao Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Teste Banco Master API",
        Version = "v1",
        Description = "API para cálculo de rotas de viagem mais econômicas",
        Contact = new OpenApiContact
        {
            Name = "Leonardo Germano dos Santos Peixe",
            Email = "dev.leonardopeixe@outlook.com"
        }
    });
});

// Configuração das Interfaces
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<IRouteService, RouteService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
