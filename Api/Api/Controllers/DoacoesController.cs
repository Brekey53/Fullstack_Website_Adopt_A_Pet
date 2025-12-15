using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAndreLeonorProjetoFinal.Models;
using ApiAndreLeonorProjetoFinal.Data;

namespace ApiAndreLeonorProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoacoesController : ControllerBase
    {
        private readonly CroaeDbContext _context;

        public DoacoesController(CroaeDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DoacaoDTO dto)
        {
            var doacao = new Doacoes
            {
                NomeDoador = dto.NomeDoador,
                TipoDoacao = dto.TipoDoacao,
                Valor = dto.Valor,
                Descricao = dto.Descricao,
                FuncionarioId = null,
                DataDoacao = DateOnly.FromDateTime(dto.Data)
            };

            _context.Doacoes.Add(doacao);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Doação registada com sucesso" });
        }
    }
}