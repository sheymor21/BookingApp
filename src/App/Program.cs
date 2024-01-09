using System.Text.Json;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration["ConnectionString"] ??
                       builder.Configuration.GetConnectionString("Postgres");

builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IUserServices, UserServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();