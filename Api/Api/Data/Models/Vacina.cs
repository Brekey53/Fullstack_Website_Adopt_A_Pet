using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Data.Models;

public partial class Vacina
{
    public int VacinaId { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Vacinaco> Vacinacos { get; set; } = new List<Vacinaco>();
}
