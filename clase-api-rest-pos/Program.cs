using clase_api_rest_pos.Mediadores;
using clase_api_rest_pos.Persistencia;
using clase_api_rest_pos.Servicios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//string stringConnection = Environment.GetEnvironmentVariable("StringConnection");
string stringConnection = "Server=localhost;Database=CursoApi;User Id=SA;Password=Ab123456*;TrustServerCertificate=true";

builder.Services.AddDbContext<PosDbContext>(options =>
{
    options.UseSqlServer(stringConnection);
});

builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AgregarMediadores();

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
