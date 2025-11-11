using System;
using System.Collections.Generic;

namespace ApiAndreLeonorProjetoFinal.Models;

public partial class Caes
{
    public int CaoId { get; set; }

    public int? RacaId { get; set; }

    public string Nome { get; set; } = null!;

    public DateOnly DataNascimento { get; set; }

    public string Porte { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public DateTime DataEntrada { get; set; }

    public string? CruzamentoRaca { get; set; }

    public int? BoxId { get; set; }

    public bool Castrado { get; set; }

    public string Caracteristica { get; set; } = null!;

    public bool? Disponivel { get; set; }

    public virtual ICollection<Adocoes> Adocoes { get; set; } = new List<Adocoes>();

    public virtual ICollection<Apadrinhamento> Apadrinhamentos { get; set; } = new List<Apadrinhamento>();

    public virtual Box? Box { get; set; }

    public virtual ICollection<ConsultasVeterinario> ConsultasVeterinarios { get; set; } = new List<ConsultasVeterinario>();

    public virtual ICollection<Foto> Fotos { get; set; } = new List<Foto>();

    public virtual ICollection<OcorrenciasCaoVoluntario> OcorrenciasCaoVoluntarios { get; set; } = new List<OcorrenciasCaoVoluntario>();

    public virtual ICollection<OcorrenciasEntreCaes> OcorrenciasEntreCaeCaoAgressorNavigations { get; set; } = new List<OcorrenciasEntreCaes>();

    public virtual ICollection<OcorrenciasEntreCaes> OcorrenciasEntreCaeCaoEnvolvidoNavigations { get; set; } = new List<OcorrenciasEntreCaes>();

    public virtual ICollection<OrdensTribunal> OrdensTribunals { get; set; } = new List<OrdensTribunal>();

    public virtual Raca? Raca { get; set; }

    public virtual ICollection<Vacinacao> Vacinacos { get; set; } = new List<Vacinacao>();
}
