using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Models;

public partial class FamiliaAdotante
{
    public int FamiliaId { get; set; }

    public string Nome { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telemovel { get; set; }

    public DateOnly? DataRegisto { get; set; }

    public virtual ICollection<Adocoes> Adocoes { get; set; } = new List<Adocoes>();
}
