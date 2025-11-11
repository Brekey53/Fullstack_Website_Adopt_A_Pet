using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Models;

public partial class OcorrenciasEntreCaes
{
    public int OcorrenciaCaesId { get; set; }

    public int CaoAgressor { get; set; }

    public int? CaoEnvolvido { get; set; }

    public DateTime DataOcorrencia { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual Caes CaoAgressorNavigation { get; set; } = null!;

    public virtual Caes? CaoEnvolvidoNavigation { get; set; }
}
