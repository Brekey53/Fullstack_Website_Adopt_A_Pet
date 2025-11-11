using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Data.Models;

public partial class Box
{
    public int BoxId { get; set; }

    public int? AlaId { get; set; }

    public virtual Ala? Ala { get; set; }

    public virtual ICollection<Cae> Caes { get; set; } = new List<Cae>();
}
