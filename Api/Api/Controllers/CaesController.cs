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
        public IActionResult GetCaes()
        {
            var caes = _dbContext.Caes.Include(c => c.Fotos).Select(c => new
                {
                    c.CaoId,
                    c.Nome,
                    c.Porte,
                    c.Sexo,
                    c.Disponivel,
                    c.Caracteristica,
                    Foto = c.Fotos.FirstOrDefault() != null ? c.Fotos.FirstOrDefault().Foto1
                        : "images/adotados/default.jpg"
                }).ToList();

            return Ok(caes);
        }


        [HttpGet("/{id}")] // assim ou como o prof fez
        public JsonResult GetId(int id)
        {
            var result = _dbContext.Caes.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }
    }
}
