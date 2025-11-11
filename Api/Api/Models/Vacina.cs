using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Models;

public partial class Vacina
{
    public int VacinaId { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Vacinacao> Vacinacos { get; set; } = new List<Vacinacao>();
}
