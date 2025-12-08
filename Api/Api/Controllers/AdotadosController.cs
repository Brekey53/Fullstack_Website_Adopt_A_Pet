using ApiAndreLeonorProjetoFinal.Data;
using ApiAndreLeonorProjetoFinal.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Polly;
using System;

[ApiController]
[Route("api/[controller]")]
public class AdotadosController : ControllerBase
{
    private readonly CroaeDbContext _dbContext;

    // Dependência de cache
    private readonly IAsyncPolicy<List<CaoDto>> _cachePolicy;
    private readonly IDistributedCache _distributedCache;

    // Chave de cache
    public const string CaesCacheKey = "lista_caes_adotados";

    public AdotadosController(CroaeDbContext context, IDistributedCache distributedCache, IAsyncPolicy<List<CaoDto>> cachePolicy)
    {
        _dbContext = context;
        _distributedCache = distributedCache;
        _cachePolicy = cachePolicy;

    }

    // GET: api/adotados
    [HttpGet]
    public async Task<IActionResult> GetAdotados()
    {

        var caesDisponiveisA = await _cachePolicy.ExecuteAsync(async context =>
        {
            // Obter do Cache L2
            var caesJsonRedis = await _distributedCache.GetStringAsync(CaesCacheKey);
            if (!string.IsNullOrEmpty(caesJsonRedis))
            {
                return JsonConvert.DeserializeObject<List<CaoDto>>(caesJsonRedis);
            }

            // Não está em cache. Ir à Base de Dados

            var caesDisponiveis = await _dbContext.Caes.Include(c => c.Fotos)
                    .Where(c => c.Disponivel == false)
                    .Select(c => new CaoDto
                    {
                        CaoId = c.CaoId,
                        Nome = c.Nome,
                        DataNascimento = c.DataNascimento,
                        Porte = c.Porte,
                        Sexo = c.Sexo,
                        Castrado = c.Castrado,
                        Disponivel = c.Disponivel,
                        Caracteristica = c.Caracteristica,
                        Foto = c.Fotos.Select(f => f.Foto1).FirstOrDefault() ?? "images/adotados/default.jpg"
                    }).ToListAsync();

            // Guardar o resultado nos Caches (L2 e L1)

            // Serializar para guardar no Redis
            var caesParaCacheJson = JsonConvert.SerializeObject(caesDisponiveis);

            // Definir opções de expiração para o Redis (ex: 30 minutos)
            var opcoesRedis = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };

            // Guardar no L2 (Redis)
            await _distributedCache.SetStringAsync(CaesCacheKey, caesParaCacheJson, opcoesRedis);


            return caesDisponiveis;

        }, new Context(CaesCacheKey));

        // Devolver o resultado acabado de ir buscar à BD
        return Ok(caesDisponiveisA);

    }

}
