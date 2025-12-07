using ApiAndreLeonorProjetoFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace ApiAndreLeonorProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FomularioContactoController : ControllerBase
    {
        [HttpPost("enviar")]
        public IActionResult Enviar([FromForm] FormularioContacto form)
        {
            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress("leonorjoaquim98@gmail.com", "CROAE");
                mail.To.Add(form.Email);
                mail.Subject = $"Resposta ao contacto: {form.Nome}";
                mail.Body = $"Olá {form.Nome},\n\nObrigado pelo teu contacto!\nTipo de visita escolhido: {form.TipoVisita}";
                mail.IsBodyHtml = false;

                using var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("leonorjoaquim98@gmail.com", "ADICIONAR CHAVE AQUI"),
                    EnableSsl = true
                };

                smtp.Send(mail);

                return Ok("Email enviado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao enviar email: {ex.Message}");
            }
        }
    }
}
