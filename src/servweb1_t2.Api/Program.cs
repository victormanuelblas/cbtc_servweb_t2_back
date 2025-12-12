using DotNetEnv;
using servweb1_t2.Application;
using servweb1_t2.Infrastructure;

var currentDir = Directory.GetCurrentDirectory();
var envPath = Path.Combine(currentDir, ".env");
Console.WriteLine($"prim: {envPath}");
if (File.Exists(envPath))
{
    Env.Load(envPath);
}else
{
    Console.WriteLine(".env file not found.");
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddControllers();

// Swagger
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


