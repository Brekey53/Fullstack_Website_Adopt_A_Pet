using ApiAndreLeonorProjetoFinal.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PagamentosController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PagamentosController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> FazerDoacao([FromBody] PagamentoDto doacao)
    {
        // Validar se vem nulo
        if (doacao == null)
        {
            return BadRequest(new { erro = "Dados inválidos." });
        }
        // Validar valor - tem de ser positivo
        if (doacao.Valor <= 0)
        {
            return BadRequest(new { erro = "O valor da doação deve ser maior que zero." });
        }
        // Validar se o numero de cartão apenas possui digitos
        if (!doacao.NumeroCartao.All(char.IsDigit))
        {
            return BadRequest(new { erro = "O cartão deve conter apenas números." });
        }
        // Verificar se não é só zeros
        if (doacao.NumeroCartao.All(c => c == '0'))
        {
            return BadRequest(new { erro = "Número de cartão inválido (zeros)." });
        }

        // Criar o cliente que aponta para o Mountebank
        var client = _httpClientFactory.CreateClient("BancoMock");

        // Dados para enviar
        var dadosPagamento = new
        {
            numeroCartao = doacao.NumeroCartao,
            valor = doacao.Valor
        };


        // Enviar pedido ao Mountebank (POST /payments)
        var response = await client.PostAsJsonAsync("payments", dadosPagamento);

        if (!response.IsSuccessStatusCode)
        {
            // Se o Mountebank devolver erro 402 (regra do cartao 0000)
            return BadRequest(new { erro = "Pagamento recusado pelo banco." });
        }

        return Ok(new { mensagem = "Doação aceite com sucesso! Obrigado " });
    }
}

public class PagamentoDto
{
    public string? NumeroCartao { get; set; }
    public decimal Valor { get; set; }
}
