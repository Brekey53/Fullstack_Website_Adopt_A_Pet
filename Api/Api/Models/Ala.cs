using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Models;

public partial class Ala
{
    public int AlaId { get; set; }

    public string? Tipo { get; set; }

    public virtual ICollection<Box> Boxes { get; set; } = new List<Box>();
}
