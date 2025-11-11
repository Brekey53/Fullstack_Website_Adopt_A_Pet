using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Data.Models;

public partial class ConsultasVeterinario
{
    public int ConsultaId { get; set; }

    public int? CaoId { get; set; }

    public int? FuncionarioId { get; set; }

    public DateTime DataConsulta { get; set; }

    public decimal Custo { get; set; }

    public string? Observacao { get; set; }

    public virtual Cae? Cao { get; set; }

    public virtual Funcionario? Funcionario { get; set; }
}
