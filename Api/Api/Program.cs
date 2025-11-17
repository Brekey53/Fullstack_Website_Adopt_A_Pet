using ApiAndreLeonorProjetoFinal.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DB
builder.Services.AddDbContext<CroaeDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Adicionar Cache L1 - Local "Polly"
builder.Services.AddMemoryCache();

// Adicionar Cache L2 - Distribuído/Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    // Pega a connection string do Redis do appsettings.json
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "ProjetoFinal_"; // Prefixo para as chaves
});

// MVC
builder.Services.AddControllers().AddNewtonsoftJson();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Static files
app.UseStaticFiles();

// Dev tools
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirection
app.UseHttpsRedirection();

// CORS
app.UseCors("PermitirTudo");

// Autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Controllers
app.MapControllers();

// run
app.Run();
