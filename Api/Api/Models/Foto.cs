using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Models;

public partial class Foto
{
    public int FotoId { get; set; }

    public int CaoId { get; set; }

    public string? Foto1 { get; set; }

    public virtual Caes Cao { get; set; } = null!;
}
