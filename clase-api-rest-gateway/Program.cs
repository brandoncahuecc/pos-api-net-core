using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

string ocelotEnv = Environment.GetEnvironmentVariable("OcelotEnvironment") ?? string.Empty;

if (!string.IsNullOrEmpty(ocelotEnv))
    ocelotEnv = $"-{ocelotEnv}";
builder.Configuration.AddJsonFile($"ocelot{ocelotEnv}.json", false, true).Build();

// Add services to the container.
builder.Services.AddOcelot();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseOcelot().Wait();
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
