using clase_api_rest_resources.Dependencias;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Logging.AgregarLogger();
builder.Services.AgregarRedisCache();

// builder.Services.AddSingleton<ICategoriaPersistencia, CategoriaPersistencia>();
// builder.Services.AddSingleton<ICategoriaServicio, CategoriaServicio>();
builder.Services.AgregarMediador<ListarCategoriaRequest>();

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
