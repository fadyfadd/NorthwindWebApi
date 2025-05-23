using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NorthWindWebApi.DataAccessLayer;
using WebApiNorthwind.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<NorthwindDataContext>((dbBuilder) => dbBuilder.UseNpgsql("Host=localhost;Port=5432;Database=northwind;Username=postgres;Password=postgres"));
builder.Services.AddAutoMapper(typeof(DefaultMapper).Assembly);
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();  
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Northwind API",
            Description = "An ASP.NET Core Web API for managing Northwind Database"
    
        });

        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
}


builder.Services.AddCors(options => {
    
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .WithOrigins("http://localhost:3000")
            .WithHeaders("Authorization", "origin", "accept", "content-type")
            .WithMethods("GET", "POST", "PUT", "DELETE")
            ;
    });
 
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}


app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();