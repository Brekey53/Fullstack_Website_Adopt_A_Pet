using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiAndreLeonorProjetoFinal.Data;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization; // [Authorize(Roles = "Administrador")] // Colocar aqui o role que pode eliminar "Administrador" ou "Admin" (exemplo)

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

        // Additional actions (GET, POST, PUT, DELETE) would go here

        [HttpGet("/api/caes")]
        public IActionResult GetCaes()
        {
            var caes = _dbContext.Caes.ToList();
            return Ok(caes);
        }


        [HttpGet("/caes/{id}")] // assim ou como o prof fez
        public JsonResult GetId(int id)
        {
            var result = _dbContext.Caes.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }
    }
}
