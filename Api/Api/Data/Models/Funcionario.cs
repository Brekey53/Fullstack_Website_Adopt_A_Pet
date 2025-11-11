using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Data.Models;

public partial class Funcionario
{
    public int FuncionarioId { get; set; }

    public string Nome { get; set; } = null!;

    public string Ocupacao { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telemovel { get; set; }

    public string Nif { get; set; } = null!;

    public virtual ICollection<ConsultasVeterinario> ConsultasVeterinarios { get; set; } = new List<ConsultasVeterinario>();

    public virtual ICollection<Doacoes> Doacos { get; set; } = new List<Doacoes>();

    public virtual ICollection<Vacinacao> Vacinacos { get; set; } = new List<Vacinacao>();
}
