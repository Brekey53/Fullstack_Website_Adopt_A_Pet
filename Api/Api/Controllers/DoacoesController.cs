using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAndreLeonorProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoacoesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DoacoesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> FazerDoacao([FromBody] DoacaoDto doacao)
        {
            // Criar o cliente que aponta para o Mountebank
            var client = _httpClientFactory.CreateClient("BancoMock");

            // Dados para enviar
            var dadosPagamento = new
            {
                numeroCartao = doacao.NumeroCartao, // Mountebank espera isto
                valor = doacao.Valor
            };

            // Enviar pedido ao Mountebank (POST /payments)
            var response = await client.PostAsJsonAsync("payments", dadosPagamento);

            if (!response.IsSuccessStatusCode)
            {
                // Se o Mountebank devolver erro 402 (regra do cartao 0000)
                return BadRequest("Pagamento Recusado pelo Banco.");
            }

            return Ok(new { mensagem = "Doação aceite com sucesso!" });
        }
    }

    public class DoacaoDto
    {
        public string NumeroCartao { get; set; }
        public decimal Valor { get; set; }
    }
}
