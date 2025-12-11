using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAndreLeonorProjetoFinal.Models;
using ApiAndreLeonorProjetoFinal.Data;

namespace ApiAndreLeonorProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacasController : ControllerBase
    {
        private readonly CroaeDbContext _context;

        public RacasController(CroaeDbContext context)
        {
            _context = context;
        }
        //api/racas 
        [HttpGet]
        public async Task<IActionResult> GetRacas()
        {
            var racas = await _context.Racas
                .Select(r => new { r.RacaId, r.Raca1 })
                .OrderBy(r => r.Raca1)
                .ToListAsync();

            return Ok(racas);
        }
    }
}
