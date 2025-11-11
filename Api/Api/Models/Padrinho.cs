using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Models;

public partial class Padrinho
{
    public int PadrinhoId { get; set; }

    public string Nome { get; set; } = null!;

    public string? Telemovel { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Apadrinhamento> Apadrinhamentos { get; set; } = new List<Apadrinhamento>();
}
