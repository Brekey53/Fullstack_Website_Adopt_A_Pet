using ApiAndreLeonorProjetoFinal.Data;
using ApiAndreLeonorProjetoFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiAndreLeonorProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PorAdotarController : ControllerBase
    {
        private readonly CroaeDbContext _dbContext;

        // Dependência de cache
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;

        // Chave de cache
        private const string CaesCacheKey = "lista_caes";

        public PorAdotarController(CroaeDbContext context, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _dbContext = context;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        // GET: api/poradotar
        [HttpGet]
        public async Task<IActionResult> GetAdotados()
        {
            // Obter do Cache L1
            if (_memoryCache.TryGetValue(CaesCacheKey, out List<CaoDto> caesDisponiveis))
            {
                // Encontra e devolve imediatamente.
                return Ok(caesDisponiveis);
            }

            // Obter do Cache L2
            var caesJsonRedis = await _distributedCache.GetStringAsync(CaesCacheKey);

            if (!string.IsNullOrEmpty(caesJsonRedis))
            {
                // Encontra o L2!
                // Desserializar o JSON do Redis
                caesDisponiveis = JsonConvert.DeserializeObject<List<CaoDto>>(caesJsonRedis);

                // Guardar no L1 (Memória) para o próximo pedido ser mais rápido
                _memoryCache.Set(CaesCacheKey, caesDisponiveis, TimeSpan.FromMinutes(5)); // Expirar L1 em 5 min

                return Ok(caesDisponiveis);
            }

            // Não está em cache. Ir à Base de Dados

            caesDisponiveis = await _dbContext.Caes.Where(c => c.Disponivel == true).Include(c => c.Fotos).Include(c => c.Raca).Select(c => new CaoDto
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

            // Guardar o resultado nos Caches (L2 e L1)

            // Serializar para guardar no Redis
            var caesParaCacheJson = JsonConvert.SerializeObject(caesDisponiveis);

            // Definir opções de expiração para o Redis (ex: 30 minutos)
            var opcoesRedis = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            };

            // Guardar no L2 (Redis)
            await _distributedCache.SetStringAsync(CaesCacheKey, caesParaCacheJson, opcoesRedis);

            // Guardar também no L1 (Memória)
            _memoryCache.Set(CaesCacheKey, caesDisponiveis, TimeSpan.FromMinutes(1)); // L1 expira mais rápido

            // Devolver o resultado acabado de ir buscar à BD
            return Ok(caesDisponiveis);
        }


        /*
        * 
        * Para já comentado pois não vale a pena ter varios pedidos de ID por várias API 
        * 
        */
        //// GET: api/poradotar/1
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAdotado(int id)
        //{
        //    // Definir a Chave ÚNICA para este cão
        //    string cacheKey = $"cao_por_adotar_{id}";

        //    // Tentar Cache L1 (Memória)
        //    if (_memoryCache.TryGetValue(cacheKey, out CaoDto caoCache))
        //    {
        //        return Ok(caoCache);
        //    }

        //    // Tentar Cache L2 (Redis)
        //    var caoJsonRedis = await _distributedCache.GetStringAsync(cacheKey);
        //    if (!string.IsNullOrEmpty(caoJsonRedis))
        //    {
        //        caoCache = JsonConvert.DeserializeObject<CaoDto>(caoJsonRedis);
        //        _memoryCache.Set(cacheKey, caoCache, TimeSpan.FromMinutes(5));
        //        return Ok(caoCache);
        //    }

        //    // Não está em cache. Ir à Base de Dados
        //    var cao = await _dbContext.Caes.Include(c => c.Fotos)
        //        .Where(c => c.CaoId == id)
        //        .Select(c => new CaoDto
        //        {
        //            CaoId = c.CaoId,
        //            Nome = c.Nome,
        //            DataNascimento = c.DataNascimento,
        //            Porte = c.Porte,
        //            Sexo = c.Sexo,
        //            Castrado = c.Castrado,
        //            Disponivel = c.Disponivel,
        //            Caracteristica = c.Caracteristica,
        //            Foto = c.Fotos.Select(f => f.Foto1).FirstOrDefault() ?? "images/adotados/default.jpg"
        //        })
        //        .FirstOrDefaultAsync(); // Aqui já tens o objeto ou null

        //    if (cao == null)
        //        return NotFound("Cão não encontrado.");

        //    // Guardar nos Caches (L2 e L1)
        //    var caoParaCacheJson = JsonConvert.SerializeObject(cao);

        //    var opcoesRedis = new DistributedCacheEntryOptions
        //    {
        //        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        //    };

        //    await _distributedCache.SetStringAsync(cacheKey, caoParaCacheJson, opcoesRedis);
        //    _memoryCache.Set(cacheKey, cao, TimeSpan.FromMinutes(5));

        //    return Ok(cao);
        //}
    }
}
