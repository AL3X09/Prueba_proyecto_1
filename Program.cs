using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Prueba_proyecto_1.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    var jsonInputFormatter = options.InputFormatters
                        .OfType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>()
                        .Single();
    jsonInputFormatter.SupportedMediaTypes
        .Add("application/x-www-form-urlencoded");
}
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //options.SwaggerDoc("v1", new OpenApiInfo { Title = "Servicio RestFul RIPS", Version = "v1" });
    options.SwaggerDoc("v1",
        new OpenApiInfo()
        {
            Title = "Servicio Libros",
            Version = "0.0.1",
            Description = "Un Servicio para Prueba de Ingreso",
            //TermsOfService = new Uri("https://example.com/terms"),//terminos de uso
            Contact = new OpenApiContact()
            {
                Email = "alex-cs09@hotmail.com",
                Name = "Alexander Cifuentes Sanchez",
            }
        });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.AddSecurityDefinition("XApiKey", new OpenApiSecurityScheme
    {
        Description = "Acceso solo con credenciales",
        Type = SecuritySchemeType.ApiKey,
        Name = "XApiKey",
        In = ParameterLocation.Header,
        //Scheme = "ApiKeyScheme"
    });
    var key = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "XApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
                    {
                             { key, new List<string>() }
                    };
    options.AddSecurityRequirement(requirement);
    //other configs;
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

#region conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Proyecto1DbContext>(options =>
    options.UseSqlServer(connectionString));
#endregion

#region configuración de los CORS
builder.Services.AddCors(p => p.AddPolicy("corspublico", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app cors
app.UseCors("corspublico");

app.UseAuthorization();

app.MapControllers();

app.Run();
