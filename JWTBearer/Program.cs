using JWTBearer.DataContext;
using JWTBearer.Repositories;
using JWTBearer.Repositories.Interfaces;
using JWTBearer.Services;
using JWTBearer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Version = "v1",
    Title = "Admin.API",
    Description = "Autenticação JWT"
  });

  options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Cabeçalho de Autorização JWT está usando o esquema de Bearer \r\n\r\nDigite o 'Bearer' antes de colocar o Token"
  });

  options.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        }
      },
      Array.Empty<string>()
    }
  });
});

builder.Services.AddAuthentication(x =>
{
  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration.GetSection("JWT")["Issuer"],
    ValidAudience = builder.Configuration.GetSection("JWT")["Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT")["Key"]))
  };
});

builder.Services.AddDbContext<UserContext>(options =>
{
  var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
  options.UseSqlServer(defaultConnection);
});

/** Dependency Injection */
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
