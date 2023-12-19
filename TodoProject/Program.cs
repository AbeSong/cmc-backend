

using Microsoft.EntityFrameworkCore;
using TodoProject.Data;
using TodoProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Connection String
var connectionString = builder.Configuration.GetConnectionString("SimpleDB");
builder.Services.AddDbContextPool<SimpleDbContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedDataRole.Initialize(services);
    await SeedDataUserProfile.Initialize(services);
    await SeedDataTodo.Initialize(services);
}

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
