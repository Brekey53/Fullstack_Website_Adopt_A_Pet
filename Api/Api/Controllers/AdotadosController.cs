using ApiAndreLeonorProjetoFinal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using System;
using ApiAndreLeonorProjetoFinal.Models;

[ApiController]
[Route("api/[controller]")]
public class AdotadosController : ControllerBase
{
    private readonly CroaeDbContext _dbContext;

    public AdotadosController(CroaeDbContext context)
    {
        _dbContext = context;
    }

    // GET: api/adotados
    [HttpGet]
    public async Task<IActionResult> GetAdotados()
    {
        var caesDisponiveis = await _dbContext.Caes.Include(c => c.Fotos)
            .Where(c => c.Disponivel == true)
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
                Foto = c.Fotos.FirstOrDefault() != null? c.Fotos.FirstOrDefault().Foto1 : "images/adotados/default.jpg"
            }).ToListAsync();


        return Ok(caesDisponiveis);

    }

    // GET: api/adotados/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdotado(int id)
    {
        var cao = await _dbContext.Caes.Include(c => c.Fotos)
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
                Foto = c.Fotos.Select(f => f.Foto1).FirstOrDefault() ?? "images/adotados/default.jpg"
            }).FirstOrDefaultAsync();

        if (cao == null)
            return NotFound("Cão não encontrado.");

        return Ok(cao);
    }

    /* Para ja nao utilizar pois este controller servira apenas para leitura dos disponiveis para adotar
    // Patch: api/adotados/1
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchAdotado(int id, [FromBody] JsonPatchDocument<Caes> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest("O documento de patch não pode ser nulo.");
        }

        // 1. Encontrar a entidade ORIGINAL no banco de dados
        var caoParaAtualizar = await _dbContext.Caes.FindAsync(id);

        if (caoParaAtualizar == null)
        {
            return NotFound("Cão não encontrado.");
        }

        // 2. Aplicar as alterações do 'patchDoc' ao objeto que veio da BD
        patchDoc.ApplyTo(caoParaAtualizar, error =>
        {
            ModelState.AddModelError(string.Empty, error.ErrorMessage);
        });

        // 3. Validar o modelo DEPOIS de aplicar o patch
        if (!TryValidateModel(caoParaAtualizar))
        {
            return ValidationProblem(ModelState); // Devolve 400 Bad Request com os erros
        }

        // 4. Salvar as alterações na Base de Dados
        await _dbContext.SaveChangesAsync();

        // 5. Retornar "Sem Conteúdo", que é o padrão para um PATCH bem-sucedido
        return NoContent(); // HTTP 204
    }
    */


}
