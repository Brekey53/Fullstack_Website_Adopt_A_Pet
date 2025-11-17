using Microsoft.AspNetCore.Mvc;
using ApiAndreLeonorProjetoFinal.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiAndreLeonorProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaesController : ControllerBase
    {
        private readonly CroaeDbContext _dbContext;

        public CaesController(CroaeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCaes()
        {
            var caesDisponiveis = await _dbContext.Caes.Include(c => c.Fotos)
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
                    Foto = c.Fotos.FirstOrDefault() != null ? c.Fotos.FirstOrDefault().Foto1 : "images/adotados/default.jpg"
                }).ToListAsync();


            return Ok(caesDisponiveis);

        }


        [HttpGet("/{id}")] // assim ou como o prof fez
        public async Task<IActionResult> GetCao(int id)
        {
            var caoId = await _dbContext.Caes.Include(c => c.Fotos)
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
                    Foto = c.Fotos.FirstOrDefault() != null ? c.Fotos.FirstOrDefault().Foto1 : "images/adotados/default.jpg"
                }).ToListAsync();

            if (caoId == null)
                return NotFound("Cão não encontrado.");

            return Ok(caoId);
        }
    }
}
