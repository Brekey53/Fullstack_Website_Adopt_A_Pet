using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Data.Models;

public partial class Voluntario
{
    public int VoluntarioId { get; set; }

    public string Nome { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telemovel { get; set; }

    public string Disponibilidade { get; set; } = null!;

    public virtual ICollection<OcorrenciasCaoVoluntario> OcorrenciasCaoVoluntarios { get; set; } = new List<OcorrenciasCaoVoluntario>();
}
