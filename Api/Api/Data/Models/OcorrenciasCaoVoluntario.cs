using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Data.Models;

public partial class OcorrenciasCaoVoluntario
{
    public int OcorrenciasCaoVoluntarioId { get; set; }

    public int? CaoId { get; set; }

    public int? VoluntarioId { get; set; }

    public DateTime DataOcorrencia { get; set; }

    public string? Descricao { get; set; }

    public bool? SeguroAtivado { get; set; }

    public virtual Caes? Cao { get; set; }

    public virtual Voluntario? Voluntario { get; set; }
}
