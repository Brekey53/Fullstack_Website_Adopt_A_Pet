using ApiAndreLeonorProjetoFinal.Data;
using ApiAndreLeonorProjetoFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Polly;

namespace ApiAndreLeonorProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PorAdotarController : ControllerBase
    {
        private readonly CroaeDbContext _dbContext;

        // Dependência de cache
        private readonly IAsyncPolicy<List<CaoDto>> _cachePolicy;
        private readonly IDistributedCache _distributedCache;

        // Chave de cache
        public const string CaesCacheKey = "lista_caes_por_adotar";

        public PorAdotarController(CroaeDbContext context, IDistributedCache distributedCache, IAsyncPolicy<List<CaoDto>> cachePolicy )
        {
            _dbContext = context;
            _distributedCache = distributedCache;
            _cachePolicy = cachePolicy;
        }

        // GET: api/poradotar
        [HttpGet]
        public async Task<IActionResult> GetAdotados()
        {
            // A Policy vai verificar automaticamente a Cache L1 (Memória).
            // Se não existir, executa a função dentro (que vai ao Redis ou BD) se existir, devolve logo o valor da memória.
            var caesDisponiveis = await _cachePolicy.ExecuteAsync(async context =>
            {
                // Obter do Cache L2
                var caesJsonRedis = await _distributedCache.GetStringAsync(CaesCacheKey);
                if (!string.IsNullOrEmpty(caesJsonRedis))
                {
                    return JsonConvert.DeserializeObject<List<CaoDto>>(caesJsonRedis);
                }

                // Não está em cache. Ir à Base de Dados

                var caesDaBd = await _dbContext.Caes.Where(c => c.Disponivel == true).Include(c => c.Fotos).Include(c => c.Raca).Select(c => new CaoDto
                {
                    CaoId = c.CaoId,
                    Nome = c.Nome,
                    DataNascimento = c.DataNascimento,
                    Porte = c.Porte,
                    Sexo = c.Sexo,
                    Castrado = c.Castrado,
                    Disponivel = c.Disponivel,
                    Caracteristica = c.Caracteristica,
                    Foto = c.Fotos.Select(f => f.Foto1).FirstOrDefault() ?? "images/adotados/default.jpg",
                    Raca = c.RacaId == 16 ? (c.CruzamentoRaca ?? "Rafeiro") : (c.Raca != null ? c.Raca.Raca1 : "Desconhecida")
                }).ToListAsync();

                // Guardar o resultado na Cache (L2)

                // Serializar para guardar no Redis
                var caesParaCacheJson = JsonConvert.SerializeObject(caesDaBd);

                // Definir opções de expiração para o Redis (ex: 30 minutos)
                var opcoesRedis = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                };

                // Guardar no L2 (Redis)
                await _distributedCache.SetStringAsync(CaesCacheKey, caesParaCacheJson, opcoesRedis);

                return caesDaBd;

            }, new Context(CaesCacheKey)); // Contexto com a chave de cache
            // Devolver o resultado acabado de ir buscar à BD
            return Ok(caesDisponiveis);


        }
    }
}
