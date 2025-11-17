using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAndreLeonorProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return Unauthorized("Utilizador não encontrado");

            // Verificar password (hash)
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Password incorrecta");

            var token = GerarJwtToken(user);
            return Ok(new { token });
        }

    }
}
