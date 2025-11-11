using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Models;

public partial class Adocoes
{
    public int AdocaoId { get; set; }

    public int? CaoId { get; set; }

    public int? FamiliaId { get; set; }

    public DateOnly DataInicioProcesso { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Caes? Cao { get; set; }

    public virtual FamiliaAdotante? Familia { get; set; }
}
