using ApiAndreLeonorProjetoFinal.Data;
using ApiAndreLeonorProjetoFinal.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Polly.Caching;
using Polly.Caching.Memory;
using Polly.Extensions.Http;
using Polly.Registry;
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
builder.Services.AddSingleton<IAsyncCacheProvider, MemoryCacheProvider>();

// Definir a política de cache (expira em 5 minutos) - Politica generica para List<CaoDto>, pois todos os Endpoints que retornam listas de cães usam o mesmo cache

// Polly para lista cães
builder.Services.AddSingleton<IAsyncPolicy<List<CaoDto>>>(serviceProvider =>
{
    var cacheProvider = serviceProvider.GetRequiredService<IAsyncCacheProvider>();

    return Policy.CacheAsync<List<CaoDto>>(
        cacheProvider,
        TimeSpan.FromMinutes(5),
        (context, key, ex) => { } // Callback de erro
    );
});

// Polly para Cão Id
builder.Services.AddSingleton<IAsyncPolicy<CaoDto>>(serviceProvider =>
{
    var cacheProvider = serviceProvider.GetRequiredService<IAsyncCacheProvider>();
    return Policy.CacheAsync<CaoDto>(
        cacheProvider,
        TimeSpan.FromMinutes(5), // Podes dar um tempo diferente se quiseres
        (context, key, ex) => { }
    );
});


// Adicionar Cache L2 - Distribuído/Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    // Pega a connection string do Redis do appsettings.json
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "ProjetoFinal_"; // Prefixo para as chaves
});

// Para usar o json patch
builder.Services.AddControllers().AddNewtonsoftJson();

// Swagger para documentação
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Para Mock do Banco
// Definir Política de Retry (Tentar 3 vezes em caso de falha)
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError() // Trata erros 5xx e 408 (timeout)
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

// Definir Política de Circuit Breaker (Para de tentar após 5 falhas seguidas)
var circuitBreakerPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

// Registar o HttpClient com as políticas
builder.Services.AddHttpClient("BancoMock", client =>
{
    client.BaseAddress = new Uri("http://localhost:4545/");
})
.AddPolicyHandler(retryPolicy)
.AddPolicyHandler(circuitBreakerPolicy);


// CORS - para que qualquer origem comunique com a API
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Static files - imagens dos cães em wwwroot
app.UseStaticFiles();

// Dev tools - Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirection
app.UseHttpsRedirection();

// CORS - Ativar Politica criada acima
app.UseCors("PermitirTudo");

// Autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Controllers - ativar todos os controllers 
app.MapControllers();

// run
app.Run();
