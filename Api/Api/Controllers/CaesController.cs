using ApiAndreLeonorProjetoFinal.Data;
using ApiAndreLeonorProjetoFinal.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace ApiAndreLeonorProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaesController : ControllerBase
    {
        private readonly CroaeDbContext _dbContext;

        // Dependência de cache
        private readonly IMemoryCache _memoryCache; // a cache local não tem de ser polly? 
        private readonly IDistributedCache _distributedCache;

        // Chave de cache
        private const string CaesCacheKey = "lista_caes_ativos"; //talvez disponivel aqui? 

        public CaesController(CroaeDbContext dbContext, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        // Get: api/Caes
        [HttpGet]
        public async Task<IActionResult> GetCaes()
        {

            // Obter do Cache L1
            if (_memoryCache.TryGetValue(CaesCacheKey, out List<CaoDto> caes))
            {
                // Encontra e devolve imediatamente.
                return Ok(caes);
            }

            // Obter do Cache L2
            var caesJsonRedis = await _distributedCache.GetStringAsync(CaesCacheKey);

            if (!string.IsNullOrEmpty(caesJsonRedis))
            {
                // Encontra o L2!
                // Desserializar o JSON do Redis
                caes = JsonConvert.DeserializeObject<List<CaoDto>>(caesJsonRedis);

                // Guardar no L1 (Memória) para o próximo pedido ser mais rápido
                _memoryCache.Set(CaesCacheKey, caes, TimeSpan.FromMinutes(5)); // Expirar L1 em 5 min

                return Ok(caes);
            }

            // Não está em cache. Ir à Base de Dados
            caes = await _dbContext.Caes.Include(c => c.Fotos)
                .Select(c => new CaoDto // Mudar para o DTO
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
            var caesParaCacheJson = JsonConvert.SerializeObject(caes);

            // Definir opções de expiração para o Redis (ex: 30 minutos)
            var opcoesRedis = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };

            // Guardar no L2 (Redis)
            await _distributedCache.SetStringAsync(CaesCacheKey, caesParaCacheJson, opcoesRedis);

            // Guardar também no L1 (Memória)
            _memoryCache.Set(CaesCacheKey, caes, TimeSpan.FromMinutes(5)); // L1 expira mais rápido

            // Devolver o resultado acabado de ir buscar à BD
            return Ok(caes);
        }

        // Get: api/Caes/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCao(int id)
        {
            var cao = await _dbContext.Caes.Include(c => c.Fotos)
                .Where(c => c.CaoId == id)
                .Select(c => new
                {
                    c.CaoId,
                    c.Nome,
                    c.DataNascimento,
                    c.Porte,
                    c.Sexo,
                    c.Castrado,
                    c.Disponivel,
                    c.Caracteristica,
                    // Faz uma sub-consulta só pela Foto1 e usa ?? (null-coalescing)
                    Foto = c.Fotos.Select(f => f.Foto1).FirstOrDefault() ?? "images/adotados/default.jpg" // Verificar se há fotos
                }).FirstOrDefaultAsync();

            if (cao == null)
                return NotFound("Cão não encontrado.");

            return Ok(cao);
        }

        // Post: api/Caes
        [HttpPost]
        public async Task<IActionResult> PostCao([FromBody] Caes cao)
        {

            // Fazer verificação se já existe um cão com o mesmo nome, data de nascimento e porte
            var caoExistente = await _dbContext.Caes.FirstOrDefaultAsync(c => c.Nome == cao.Nome && c.DataNascimento == cao.DataNascimento && c.Porte == cao.Porte);
            if (caoExistente != null)
            {
                return Conflict("Já existe um cão com essa caracteristicas na base de dados");
            }

            // Adicionar o novo cão à base de dados
            await _dbContext.Caes.AddAsync(cao);

            // Guardar as alterações
            await _dbContext.SaveChangesAsync();

            // Invalida o cache de cães para garantir que a lista é atualizada
            await InvalidateCacheAsync();

            // Retorna 201 Created + URL para o novo Cão e objeto Cão
            return CreatedAtAction(nameof(GetCao), new { id = cao.CaoId }, cao);


        }

        // Put: api/Caes/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCao(int id, [FromBody] Caes cao)
        {
            // 1. Validar se o ID do URL corresponde ao ID do objeto
            if (id != cao.CaoId)
            {
                return BadRequest("O ID do URL não corresponde ao ID do objeto.");
            }

            // 2. Encontrar o cão na banco de dados
            var caoParaAtualizar = await _dbContext.Caes.FindAsync(id);

            if (caoParaAtualizar == null)
            {
                return NotFound("Cão não encontrado.");
            }

            // 3. Aplicar o PUT
            // Copiamos TODOS os valores do objeto 'cao' (do Body)
            // para o objeto 'caoParaAtualizar' (que veio da BD)
            // O Entity Framework vai detetar estas mudanças.

            caoParaAtualizar.Nome = cao.Nome;
            caoParaAtualizar.DataNascimento = cao.DataNascimento;
            caoParaAtualizar.Porte = cao.Porte;
            caoParaAtualizar.Sexo = cao.Sexo;
            caoParaAtualizar.Castrado = cao.Castrado;
            caoParaAtualizar.Disponivel = cao.Disponivel;
            caoParaAtualizar.Caracteristica = cao.Caracteristica;
            // Nota: Não atualizamos o CaoId, pois é a chave primária.

            // 4. Validar o modelo DEPOIS de aplicar as alterações
            if (!TryValidateModel(caoParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            // 5. Salvar as alterações na Base de Dados
            try
            {
                await _dbContext.SaveChangesAsync();

                // Invalida o cache de cães para garantir que a lista é atualizada
                await InvalidateCacheAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Isto é raro, mas acontece se alguém apagar o cão 
                // entre o FindAsync() e o SaveChangesAsync()
                if (!_dbContext.Caes.Any(e => e.CaoId == id))
                {
                    return NotFound("Conflito: Cão foi apagado da base de dados por outro utilizador.");
                }
                else
                {
                    throw;
                }
            }

            // 6. Retornar "Sem Conteúdo", que é o padrão para um PUT/PATCH bem-sucedido
            return NoContent(); // HTTP 204
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCao(int id)
        {
            var cao = await _dbContext.Caes.FindAsync(id);

            if (cao == null)
            {
                return NotFound("Cão não encontrado na Base de Dados.");
            }

            _dbContext.Caes.Remove(cao);

            // guardar as alterações na BD
            await _dbContext.SaveChangesAsync();

            // Invalida o cache de cães para garantir que a lista é atualizada
            await InvalidateCacheAsync();

            return NoContent(); // HTTP 204

        }

        // Patch: api/caes/1
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAdotado(int id, [FromBody] JsonPatchDocument<Caes> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("O documento de patch não pode ser nulo.");
            }

            // Encontrar a entidade ORIGINAL no banco de dados
            var caoParaAtualizar = await _dbContext.Caes.FindAsync(id);

            if (caoParaAtualizar == null)
            {
                return NotFound("Cão não encontrado.");
            }

            // Aplicar as alterações do 'patchDoc' ao objeto que veio da BD
            patchDoc.ApplyTo(caoParaAtualizar, error =>
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            });

            // Validar o modelo DEPOIS de aplicar o patch
            if (!TryValidateModel(caoParaAtualizar))
            {
                return ValidationProblem(ModelState); // Devolve 400 Bad Request com os erros
            }

            // Salvar as alterações na Base de Dados
            await _dbContext.SaveChangesAsync();

            // Invalida o cache de cães para garantir que a lista é atualizada
            await InvalidateCacheAsync();

            // Retornar "Sem Conteúdo", que é o padrão para um PATCH bem-sucedido
            return NoContent(); // HTTP 204
        }

        /// <summary>
        /// Remove a chave de cache da lista de cães (L1 e L2)
        /// </summary>
        private async Task InvalidateCacheAsync()
        {
            // Remove do L2 (Redis)
            await _distributedCache.RemoveAsync(CaesCacheKey);

            // Remove do L1 (Memória)
            _memoryCache.Remove(CaesCacheKey);
        }
    }
}
