using Adres.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Adres.Application.Common.Mappings;
using Adres.Application.Services;
using FluentValidation;
using Adres.Infrastructure.Repositories;
using Adres.Application.Validators;
using Adres.Domain.Common;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Adres API",
        Version = "v1",
        Description = "API para el sistema de gestión de adquisiciones",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Equipo de Desarrollo",
            Email = "desarrollo@adres.com"
        }
    });
});

// Configurar DbContext con la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("No se ha encontrado la cadena de conexión 'DefaultConnection' en la configuración.");
}

builder.Services.AddDbContext<AdresDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registrar AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Registrar FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateAdquisicionValidator>();

// Registrar repositorios
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

// Registrar servicios
builder.Services.AddScoped<IAdquisicionService, AdquisicionService>();
builder.Services.AddScoped<IUnidadAdministrativaService, UnidadAdministrativaService>();
builder.Services.AddScoped<ITipoBienServicioService, TipoBienServicioService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IDocumentacionService, DocumentacionService>();
builder.Services.AddScoped<IHistorialService, HistorialService>();

var app = builder.Build();

// Configurar el pipeline de HTTP request
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configurar el pipeline de la aplicación
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run(); 