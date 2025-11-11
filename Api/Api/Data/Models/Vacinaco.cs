using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Data.Models;

public partial class Vacinaco
{
    public int VacinacaoId { get; set; }

    public int CaoId { get; set; }

    public int? FuncionarioId { get; set; }

    public int? VacinaId { get; set; }

    public DateTime DataVacinacao { get; set; }

    public virtual Cae Cao { get; set; } = null!;

    public virtual Funcionario? Funcionario { get; set; }

    public virtual Vacina? Vacina { get; set; }
}
