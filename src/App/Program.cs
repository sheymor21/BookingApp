using System.Reflection;
using System.Text.Json;
using BookingApp.Configuration;
using BookingApp.Settings;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
builder.Services.DepedencyInjection();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Booking App",
        Description = "This is a booking API for my CV",
        Version = "V1",
        Contact = new OpenApiContact
        {
            Name = "Jose Armando",
            Url = new Uri(
                "https://www.linkedin.com/in/jose-armando-coronel-vasquez-54a3772a8?lipi=urn%3Ali%3Apage%3Ad_flagship3_profile_view_base_contact_details%3BDcwQi%2BKuSxCulxusVlwFZQ%3D%3D"),
            Email = "joseacvz81@gmail.com",
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.Migrations();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();