using ApiAndreLeonorProjetoFinal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

[ApiController]
[Route("api/[controller]")]
public class AdotadosController : ControllerBase
{
    private readonly CroaeDbContext _context;

    public AdotadosController(CroaeDbContext context)
    {
        _context = context;
    }

    // GET: api/adotados
    [HttpGet]
    public async Task<IActionResult> GetAdotados()
    {
        var animais = await _context.Caes.ToListAsync();
        return Ok(animais);
    }

    // GET: api/adotados/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdotado(int id)
    {
        var animal = await _context.Caes.FindAsync(id);
        if (animal == null)
            return NotFound();

        return Ok(animal);
    }
}
