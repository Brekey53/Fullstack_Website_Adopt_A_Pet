using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Data.Models;

public partial class Raca
{
    public int RacaId { get; set; }

    public string Raca1 { get; set; } = null!;

    public virtual ICollection<Cae> Caes { get; set; } = new List<Cae>();
}
