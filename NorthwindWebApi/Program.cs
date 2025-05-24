using System.Net.Mime;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NorthwindWebApi.Configuration;
using NorthWindWebApi.DataAccessLayer;
using NorthwindWebApi.Security;
using WebApiNorthwind.Mappers;

var builder = WebApplication.CreateBuilder(args);

var appConfig = builder.Configuration.GetSection("AppConfiguration").Get<AppConfiguration>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.Configure<AppConfiguration>(builder.Configuration.GetSection("AppConfiguration"));

builder.Services.AddDbContext<NorthwindDataContext>((dbBuilder) =>
    dbBuilder.UseNpgsql(appConfig.DatabaseConfiguration.ConnectionString));

builder.Services.AddAutoMapper(typeof(DefaultMapper).Assembly);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
   

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc(appConfig.SwaggerConfiguration.Version, new OpenApiInfo
        {
            Version = appConfig.SwaggerConfiguration.Version,
            Title = appConfig.SwaggerConfiguration.Title,
            Description = appConfig.SwaggerConfiguration.Description,
        });

        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
}


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .WithOrigins(appConfig.CorsConfiguration.WithOrigins)
            .WithHeaders(appConfig.CorsConfiguration.WithHeaders)
            .WithMethods(appConfig.CorsConfiguration.WithMethods)
            ;
    });
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.Password.RequiredLength = 5;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = true;
    })
    .AddEntityFrameworkStores<NorthwindDataContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, NorthwindDataContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, NorthwindDataContext, Guid>>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidAudience = appConfig.JwtConfiguration.Audience,
            ValidateIssuer = true,
            ValidIssuer = appConfig.JwtConfiguration.Issuer,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appConfig.JwtConfiguration.Key))
        };
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