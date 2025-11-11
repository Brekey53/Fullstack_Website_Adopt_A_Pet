using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Models;

public partial class Apadrinhamento
{
    public int ApadrinhamentoId { get; set; }

    public int CaoId { get; set; }

    public int PadrinhoId { get; set; }

    public DateOnly DataInicio { get; set; }

    public DateOnly? DataFim { get; set; }

    public virtual Caes Cao { get; set; } = null!;

    public virtual Padrinho Padrinho { get; set; } = null!;
}
