using ApiAndreLeonorProjetoFinal.Data;
using ApiAndreLeonorProjetoFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiAndreLeonorProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly CroaeDbContext _dbcontext;

        public LoginController(IConfiguration config, CroaeDbContext context)
        {
            _config = config;
            _dbcontext = context;
        }


        [HttpPost]
        //Recebe um JSON com a mesma informação que temos na classe Login
        public async Task<IActionResult> LoginAsync([FromBody] Login loginData)
        {
            // Verificação de campos no forms
            if (loginData == null || string.IsNullOrEmpty(loginData.Username) || string.IsNullOrEmpty(loginData.Password))
            {
                return BadRequest("Dados de login inválidos.");
            }

            // vamos à procura do funcionario na DB
            var funcionario = await _dbcontext.Funcionarios.FirstOrDefaultAsync(f => f.Email.ToLower() == loginData.Username.ToLower());
            
            // se não encontrou com aquele email dá erro, se encontrou validar a password
            if (funcionario == null || funcionario.Nif != loginData.Password)
            {
                return Unauthorized("Email ou password incorretos.");
            }


            //Se for um utilizador válido, gera o token abaixo
            var token = GenerateJwtToken(funcionario.Nome);
            return Ok(new { token }); //200 - OK com o token
        }


        // Para gerar um token aleatorio que depois é usado para requests que sejam POST, PATCH ou
        // DELETE, porque não queremos que qualquer um os faça
        private string GenerateJwtToken(string username)
        {   
            //key definida na appsettings.json
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]);

            //Configurações da chave. Este é o standart HMAC SHA256
            var securityKey = new SymmetricSecurityKey(key);
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] { new Claim (ClaimTypes.Name, username) };

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"], //api
                audience: _config["JwtSettings:Audience"], //api
                claims: claims, // informações do user - nome
                expires: DateTime.Now.AddHours(2), // quando expira
                signingCredentials: creds // assinatura acima
            );

            //token 
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
