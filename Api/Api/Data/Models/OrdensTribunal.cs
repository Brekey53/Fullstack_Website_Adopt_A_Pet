using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Data.Models;

public partial class OrdensTribunal
{
    public int OrdemId { get; set; }

    public int CaoId { get; set; }

    public string Observacao { get; set; } = null!;

    public virtual Caes Cao { get; set; } = null!;
}
