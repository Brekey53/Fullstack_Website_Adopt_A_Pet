using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Data.Models;

public partial class Doacoes
{
    public int DoacaoId { get; set; }

    public string NomeDoador { get; set; } = null!;

    public int? FuncionarioId { get; set; }

    public DateOnly DataDoacao { get; set; }

    public string TipoDoacao { get; set; } = null!;

    public decimal? Valor { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual Funcionario? Funcionario { get; set; }
}
