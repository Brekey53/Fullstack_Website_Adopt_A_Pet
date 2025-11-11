using System;
using System.Collections.Generic;
using ApiAndreLeonorProjetoFinal.Data.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ApiAndreLeonorProjetoFinal.Data;

public partial class CroaeDbContext : DbContext
{
    public CroaeDbContext()
    {
    }

    public CroaeDbContext(DbContextOptions<CroaeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adocoes> Adocoes { get; set; }

    public virtual DbSet<Ala> Alas { get; set; }

    public virtual DbSet<Apadrinhamento> Apadrinhamentos { get; set; }

    public virtual DbSet<Box> Boxes { get; set; }

    public virtual DbSet<Caes> Caes { get; set; }

    public virtual DbSet<ConsultasVeterinario> ConsultasVeterinarios { get; set; }

    public virtual DbSet<Doacoes> Doacoes { get; set; }

    public virtual DbSet<FamiliaAdotante> FamiliaAdotantes { get; set; }

    public virtual DbSet<Foto> Fotos { get; set; }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }

    public virtual DbSet<OcorrenciasCaoVoluntario> OcorrenciasCaoVoluntarios { get; set; }

    public virtual DbSet<OcorrenciasEntreCaes> OcorrenciasEntreCaes { get; set; }

    public virtual DbSet<OrdensTribunal> OrdensTribunals { get; set; }

    public virtual DbSet<Padrinho> Padrinhos { get; set; }

    public virtual DbSet<Raca> Racas { get; set; }

    public virtual DbSet<Vacina> Vacinas { get; set; }

    public virtual DbSet<Vacinacao> Vacinacoes { get; set; }

    public virtual DbSet<Voluntario> Voluntarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=croae_projeto;user=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.3.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Adocoes>(entity =>
        {
            entity.HasKey(e => e.AdocaoId).HasName("PRIMARY");

            entity.ToTable("adocoes");

            entity.HasIndex(e => e.CaoId, "fk_adocoes_caes");

            entity.HasIndex(e => e.FamiliaId, "fk_adocoes_familias");

            entity.Property(e => e.AdocaoId).HasColumnName("adocao_id");
            entity.Property(e => e.CaoId).HasColumnName("cao_id");
            entity.Property(e => e.DataInicioProcesso).HasColumnName("data_inicio_processo");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'A decorrer'")
                .HasColumnType("enum('Concluida','Cancelada','A decorrer')")
                .HasColumnName("estado");
            entity.Property(e => e.FamiliaId).HasColumnName("familia_id");

            entity.HasOne(d => d.Cao).WithMany(p => p.Adocos)
                .HasForeignKey(d => d.CaoId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_adocoes_caes");

            entity.HasOne(d => d.Familia).WithMany(p => p.Adocos)
                .HasForeignKey(d => d.FamiliaId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_adocoes_familias");
        });

        modelBuilder.Entity<Ala>(entity =>
        {
            entity.HasKey(e => e.AlaId).HasName("PRIMARY");

            entity.ToTable("alas");

            entity.HasIndex(e => e.Tipo, "tipo").IsUnique();

            entity.Property(e => e.AlaId).HasColumnName("ala_id");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Apadrinhamento>(entity =>
        {
            entity.HasKey(e => e.ApadrinhamentoId).HasName("PRIMARY");

            entity.ToTable("apadrinhamentos");

            entity.HasIndex(e => e.CaoId, "fk_apadrinhamentos_caes");

            entity.HasIndex(e => e.PadrinhoId, "fk_apadrinhamentos_padrinhos");

            entity.Property(e => e.ApadrinhamentoId).HasColumnName("apadrinhamento_id");
            entity.Property(e => e.CaoId).HasColumnName("cao_id");
            entity.Property(e => e.DataFim).HasColumnName("data_fim");
            entity.Property(e => e.DataInicio).HasColumnName("data_inicio");
            entity.Property(e => e.PadrinhoId).HasColumnName("padrinho_id");

            entity.HasOne(d => d.Cao).WithMany(p => p.Apadrinhamentos)
                .HasForeignKey(d => d.CaoId)
                .HasConstraintName("fk_apadrinhamentos_caes");

            entity.HasOne(d => d.Padrinho).WithMany(p => p.Apadrinhamentos)
                .HasForeignKey(d => d.PadrinhoId)
                .HasConstraintName("fk_apadrinhamentos_padrinhos");
        });

        modelBuilder.Entity<Box>(entity =>
        {
            entity.HasKey(e => e.BoxId).HasName("PRIMARY");

            entity.ToTable("boxes");

            entity.HasIndex(e => e.AlaId, "fk_boxes_alas");

            entity.Property(e => e.BoxId).HasColumnName("box_id");
            entity.Property(e => e.AlaId).HasColumnName("ala_id");

            entity.HasOne(d => d.Ala).WithMany(p => p.Boxes)
                .HasForeignKey(d => d.AlaId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_boxes_alas");
        });

        modelBuilder.Entity<Caes>(entity =>
        {
            entity.HasKey(e => e.CaoId).HasName("PRIMARY");

            entity.ToTable("caes");

            entity.HasIndex(e => e.BoxId, "fk_caes_boxes");

            entity.HasIndex(e => e.DataNascimento, "idx_data_nascimento");

            entity.HasIndex(e => e.RacaId, "idx_raca_id");

            entity.Property(e => e.CaoId).HasColumnName("cao_id");
            entity.Property(e => e.BoxId).HasColumnName("box_id");
            entity.Property(e => e.Caracteristica)
                .HasMaxLength(100)
                .HasColumnName("caracteristica");
            entity.Property(e => e.Castrado).HasColumnName("castrado");
            entity.Property(e => e.CruzamentoRaca)
                .HasMaxLength(100)
                .HasColumnName("cruzamento_raca");
            entity.Property(e => e.DataEntrada)
                .HasColumnType("datetime")
                .HasColumnName("data_entrada");
            entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");
            entity.Property(e => e.Disponivel)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("disponivel");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
            entity.Property(e => e.Porte)
                .HasColumnType("enum('Pequeno','Médio','Grande')")
                .HasColumnName("porte");
            entity.Property(e => e.RacaId).HasColumnName("raca_id");
            entity.Property(e => e.Sexo)
                .HasColumnType("enum('M','F')")
                .HasColumnName("sexo");

            entity.HasOne(d => d.Box).WithMany(p => p.Caes)
                .HasForeignKey(d => d.BoxId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_caes_boxes");

            entity.HasOne(d => d.Raca).WithMany(p => p.Caes)
                .HasForeignKey(d => d.RacaId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_caes_racas");
        });

        modelBuilder.Entity<ConsultasVeterinario>(entity =>
        {
            entity.HasKey(e => e.ConsultaId).HasName("PRIMARY");

            entity.ToTable("consultas_veterinario");

            entity.HasIndex(e => e.FuncionarioId, "fk_consultas_veterinario_funcionarios");

            entity.HasIndex(e => new { e.CaoId, e.DataConsulta }, "idx_consultas_cao_data");

            entity.Property(e => e.ConsultaId).HasColumnName("consulta_id");
            entity.Property(e => e.CaoId).HasColumnName("cao_id");
            entity.Property(e => e.Custo)
                .HasPrecision(8, 2)
                .HasColumnName("custo");
            entity.Property(e => e.DataConsulta)
                .HasColumnType("datetime")
                .HasColumnName("data_consulta");
            entity.Property(e => e.FuncionarioId).HasColumnName("funcionario_id");
            entity.Property(e => e.Observacao)
                .HasMaxLength(250)
                .HasColumnName("observacao");

            entity.HasOne(d => d.Cao).WithMany(p => p.ConsultasVeterinarios)
                .HasForeignKey(d => d.CaoId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_consultas_veterinario_caes");

            entity.HasOne(d => d.Funcionario).WithMany(p => p.ConsultasVeterinarios)
                .HasForeignKey(d => d.FuncionarioId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_consultas_veterinario_funcionarios");
        });

        modelBuilder.Entity<Doacoes>(entity =>
        {
            entity.HasKey(e => e.DoacaoId).HasName("PRIMARY");

            entity.ToTable("doacoes");

            entity.HasIndex(e => e.FuncionarioId, "fk_doacoes_funcionarios");

            entity.Property(e => e.DoacaoId).HasColumnName("doacao_id");
            entity.Property(e => e.DataDoacao).HasColumnName("data_doacao");
            entity.Property(e => e.Descricao)
                .HasMaxLength(100)
                .HasColumnName("descricao");
            entity.Property(e => e.FuncionarioId).HasColumnName("funcionario_id");
            entity.Property(e => e.NomeDoador)
                .HasMaxLength(100)
                .HasColumnName("nome_doador");
            entity.Property(e => e.TipoDoacao)
                .HasDefaultValueSql("'Bens ou Comida'")
                .HasColumnType("enum('Monetária','Bens ou Comida')")
                .HasColumnName("tipo_doacao");
            entity.Property(e => e.Valor)
                .HasPrecision(6, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.Funcionario).WithMany(p => p.Doacos)
                .HasForeignKey(d => d.FuncionarioId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_doacoes_funcionarios");
        });

        modelBuilder.Entity<FamiliaAdotante>(entity =>
        {
            entity.HasKey(e => e.FamiliaId).HasName("PRIMARY");

            entity.ToTable("familia_adotantes");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Telemovel, "telemovel").IsUnique();

            entity.Property(e => e.FamiliaId).HasColumnName("familia_id");
            entity.Property(e => e.DataRegisto).HasColumnName("data_registo");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
            entity.Property(e => e.Telemovel)
                .HasMaxLength(13)
                .HasColumnName("telemovel");
        });

        modelBuilder.Entity<Foto>(entity =>
        {
            entity.HasKey(e => e.FotoId).HasName("PRIMARY");

            entity.ToTable("fotos");

            entity.HasIndex(e => e.CaoId, "fk_fotos_caes");

            entity.Property(e => e.FotoId).HasColumnName("foto_id");
            entity.Property(e => e.CaoId).HasColumnName("cao_id");
            entity.Property(e => e.Foto1).HasColumnName("foto");

            entity.HasOne(d => d.Cao).WithMany(p => p.Fotos)
                .HasForeignKey(d => d.CaoId)
                .HasConstraintName("fk_fotos_caes");
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.FuncionarioId).HasName("PRIMARY");

            entity.ToTable("funcionarios");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Nif, "nif").IsUnique();

            entity.HasIndex(e => e.Telemovel, "telemovel").IsUnique();

            entity.Property(e => e.FuncionarioId).HasColumnName("funcionario_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Nif)
                .HasMaxLength(9)
                .HasColumnName("nif");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Ocupacao)
                .HasMaxLength(45)
                .HasColumnName("ocupacao");
            entity.Property(e => e.Telemovel)
                .HasMaxLength(13)
                .HasColumnName("telemovel");
        });

        modelBuilder.Entity<OcorrenciasCaoVoluntario>(entity =>
        {
            entity.HasKey(e => e.OcorrenciasCaoVoluntarioId).HasName("PRIMARY");

            entity.ToTable("ocorrencias_cao_voluntario");

            entity.HasIndex(e => e.CaoId, "fk_ocorrencias_caes");

            entity.HasIndex(e => e.VoluntarioId, "fk_ocorrencias_voluntarios");

            entity.Property(e => e.OcorrenciasCaoVoluntarioId).HasColumnName("ocorrencias_cao_voluntario_id");
            entity.Property(e => e.CaoId).HasColumnName("cao_id");
            entity.Property(e => e.DataOcorrencia)
                .HasColumnType("datetime")
                .HasColumnName("data_ocorrencia");
            entity.Property(e => e.Descricao)
                .HasMaxLength(500)
                .HasColumnName("descricao");
            entity.Property(e => e.SeguroAtivado)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("seguro_ativado");
            entity.Property(e => e.VoluntarioId).HasColumnName("voluntario_id");

            entity.HasOne(d => d.Cao).WithMany(p => p.OcorrenciasCaoVoluntarios)
                .HasForeignKey(d => d.CaoId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_ocorrencias_caes");

            entity.HasOne(d => d.Voluntario).WithMany(p => p.OcorrenciasCaoVoluntarios)
                .HasForeignKey(d => d.VoluntarioId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_ocorrencias_voluntarios");
        });

        modelBuilder.Entity<OcorrenciasEntreCaes>(entity =>
        {
            entity.HasKey(e => e.OcorrenciaCaesId).HasName("PRIMARY");

            entity.ToTable("ocorrencias_entre_caes");

            entity.HasIndex(e => e.CaoAgressor, "fk_ocorrencias_caes_agressor");

            entity.HasIndex(e => e.CaoEnvolvido, "fk_ocorrencias_caes_envolvido");

            entity.Property(e => e.OcorrenciaCaesId).HasColumnName("ocorrencia_caes_id");
            entity.Property(e => e.CaoAgressor).HasColumnName("cao_agressor");
            entity.Property(e => e.CaoEnvolvido).HasColumnName("cao_envolvido");
            entity.Property(e => e.DataOcorrencia)
                .HasColumnType("datetime")
                .HasColumnName("data_ocorrencia");
            entity.Property(e => e.Descricao)
                .HasMaxLength(250)
                .HasColumnName("descricao");

            entity.HasOne(d => d.CaoAgressorNavigation).WithMany(p => p.OcorrenciasEntreCaeCaoAgressorNavigations)
                .HasForeignKey(d => d.CaoAgressor)
                .HasConstraintName("fk_ocorrencias_caes_agressor");

            entity.HasOne(d => d.CaoEnvolvidoNavigation).WithMany(p => p.OcorrenciasEntreCaeCaoEnvolvidoNavigations)
                .HasForeignKey(d => d.CaoEnvolvido)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_ocorrencias_caes_envolvido");
        });

        modelBuilder.Entity<OrdensTribunal>(entity =>
        {
            entity.HasKey(e => e.OrdemId).HasName("PRIMARY");

            entity.ToTable("ordens_tribunal");

            entity.HasIndex(e => e.CaoId, "fk_ordens_tribunal_caes");

            entity.Property(e => e.OrdemId).HasColumnName("ordem_id");
            entity.Property(e => e.CaoId).HasColumnName("cao_id");
            entity.Property(e => e.Observacao)
                .HasMaxLength(250)
                .HasColumnName("observacao");

            entity.HasOne(d => d.Cao).WithMany(p => p.OrdensTribunals)
                .HasForeignKey(d => d.CaoId)
                .HasConstraintName("fk_ordens_tribunal_caes");
        });

        modelBuilder.Entity<Padrinho>(entity =>
        {
            entity.HasKey(e => e.PadrinhoId).HasName("PRIMARY");

            entity.ToTable("padrinhos");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Telemovel, "telemovel").IsUnique();

            entity.Property(e => e.PadrinhoId).HasColumnName("padrinho_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Telemovel)
                .HasMaxLength(13)
                .HasColumnName("telemovel");
        });

        modelBuilder.Entity<Raca>(entity =>
        {
            entity.HasKey(e => e.RacaId).HasName("PRIMARY");

            entity.ToTable("racas");

            entity.HasIndex(e => e.Raca1, "raca").IsUnique();

            entity.Property(e => e.RacaId).HasColumnName("raca_id");
            entity.Property(e => e.Raca1)
                .HasMaxLength(45)
                .HasColumnName("raca");
        });

        modelBuilder.Entity<Vacina>(entity =>
        {
            entity.HasKey(e => e.VacinaId).HasName("PRIMARY");

            entity.ToTable("vacinas");

            entity.Property(e => e.VacinaId).HasColumnName("vacina_id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Vacinacao>(entity =>
        {
            entity.HasKey(e => e.VacinacaoId).HasName("PRIMARY");

            entity.ToTable("vacinacoes");

            entity.HasIndex(e => e.FuncionarioId, "fk_vacinacoes_funcionarios");

            entity.HasIndex(e => e.VacinaId, "fk_vacinacoes_vacinas");

            entity.HasIndex(e => new { e.CaoId, e.DataVacinacao, e.VacinaId }, "idx_vacinacoes_cao_data");

            entity.Property(e => e.VacinacaoId).HasColumnName("vacinacao_id");
            entity.Property(e => e.CaoId).HasColumnName("cao_id");
            entity.Property(e => e.DataVacinacao)
                .HasColumnType("datetime")
                .HasColumnName("data_vacinacao");
            entity.Property(e => e.FuncionarioId).HasColumnName("funcionario_id");
            entity.Property(e => e.VacinaId).HasColumnName("vacina_id");

            entity.HasOne(d => d.Cao).WithMany(p => p.Vacinacos)
                .HasForeignKey(d => d.CaoId)
                .HasConstraintName("fk_vacinacoes_caes");

            entity.HasOne(d => d.Funcionario).WithMany(p => p.Vacinacos)
                .HasForeignKey(d => d.FuncionarioId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_vacinacoes_funcionarios");

            entity.HasOne(d => d.Vacina).WithMany(p => p.Vacinacos)
                .HasForeignKey(d => d.VacinaId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_vacinacoes_vacinas");
        });

        modelBuilder.Entity<Voluntario>(entity =>
        {
            entity.HasKey(e => e.VoluntarioId).HasName("PRIMARY");

            entity.ToTable("voluntarios");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Telemovel, "telemovel").IsUnique();

            entity.Property(e => e.VoluntarioId).HasColumnName("voluntario_id");
            entity.Property(e => e.Disponibilidade)
                .HasColumnType("enum('Sabado: 9h-12h','Domingo: 9h-12h','Feriado: 9h-12h','Fim-de-Semana','Todos','Depende')")
                .HasColumnName("disponibilidade");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Telemovel)
                .HasMaxLength(13)
                .HasColumnName("telemovel");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
