using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Data.Models;

public partial class OcorrenciasEntreCae
{
    public int OcorrenciaCaesId { get; set; }

    public int CaoAgressor { get; set; }

    public int? CaoEnvolvido { get; set; }

    public DateTime DataOcorrencia { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual Cae CaoAgressorNavigation { get; set; } = null!;

    public virtual Cae? CaoEnvolvidoNavigation { get; set; }
}
