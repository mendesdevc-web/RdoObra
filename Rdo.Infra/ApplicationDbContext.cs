using Microsoft.EntityFrameworkCore;
using Rdo.Dominio.Entidades;

namespace Rdo.Infra
{
    public class ApplicationDbContext : DbContext
    {
        public readonly IEnumerable<object> ObraUsuario;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<UsuarioEntidade> Usuarios { get; set; }
        public DbSet<ObraEntidade> Obras { get; set; }
        public DbSet<ObraUsuarioEntidade> ObrasUsuarios { get; set; }
        public DbSet<ServicoEntidade> Servicos { get; set; }
        public DbSet<DiarioEntidade> Diarios { get; set; }
        public DbSet<ApontamentoEntidade> Apontamentos { get; set; }
        public DbSet<HistoricoStatusEntidade> HistoricosStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // USUARIO
            modelBuilder.Entity<UsuarioEntidade>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(u => u.Email)
                    .HasColumnName("email")
                    .IsRequired();

                entity.Property(u => u.UsuarioNome)
                    .HasColumnName("usuario")
                    .IsRequired();

                entity.Property(u => u.Cargo)
                    .HasColumnName("cargo")
                    .IsRequired();

                entity.Property(u => u.SenhaHash)
                    .HasColumnName("senha_hash")
                    .IsRequired();

                entity.Property(u => u.SenhaSalt)
                    .HasColumnName("senha_salt")
                    .IsRequired();

                entity.Property(u => u.TokenDataCriacao)
                    .HasColumnName("token_data_criacao")
                    .IsRequired();
            });

            // OBRA
            modelBuilder.Entity<ObraEntidade>(entity =>
            {
                entity.ToTable("obras");

                entity.HasKey(o => o.Id);

                entity.Property(o => o.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(o => o.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(o => o.Endereco)
                    .HasColumnName("endereco")
                    .HasMaxLength(250)
                    .IsRequired();

                entity.Property(o => o.ResponsavelTecnico)
                    .HasColumnName("responsavel_tecnico")
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(o => o.Status)
                    .HasColumnName("status")
                    .HasMaxLength(100);

                entity.Property(o => o.DataInicio)
                    .HasColumnName("data_inicio")
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(o => o.DataFim)
                    .HasColumnName("data_fim")
                    .HasColumnType("date");
            });

            // OBRA_USUARIO
            modelBuilder.Entity<ObraUsuarioEntidade>(entity =>
            {
                entity.ToTable("obra_usuario");

                entity.HasKey(ou => ou.Id);

                entity.Property(ou => ou.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(ou => ou.ObraId)
                    .HasColumnName("obra_id")
                    .IsRequired();

                entity.Property(ou => ou.UsuarioId)
                    .HasColumnName("usuario_id")
                    .IsRequired();

                entity.Property(ou => ou.Papel)
                    .HasColumnName("papel")
                    .HasMaxLength(20)
                    .IsRequired();


                // Obra 1:N ObraUsuario
                entity.HasOne(ou => ou.Obra)
                    .WithMany(o => o.ObrasUsuarios)
                    .HasForeignKey(ou => ou.ObraId)
                    .OnDelete(DeleteBehavior.Cascade);


                // Usuario 1:N ObraUsuario
                entity.HasOne(ou => ou.Usuario)
                    .WithMany(u => u.ObrasUsuarios)
                    .HasForeignKey(ou => ou.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);


                // Evita o mesmo usuário ser vinculado duas vezes à mesma obra
                entity.HasIndex(ou => new
                {
                    ou.ObraId,
                    ou.UsuarioId
                })
                .IsUnique();
            });

            // SERVICO
            modelBuilder.Entity<ServicoEntidade>(entity =>
            {
                entity.ToTable("servicos");

                entity.HasKey(s => s.Id);

                entity.Property(s => s.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(s => s.ObraId)
                    .HasColumnName("obra_id")
                    .IsRequired();

                entity.Property(s => s.Descricao)
                    .HasColumnName("descricao")
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(s => s.UnidadeMedida)
                    .HasColumnName("unidade_medida")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(s => s.QuantidadeOrcada)
                    .HasColumnName("quantidade_orcada")
                    .HasPrecision(12, 2)
                    .IsRequired();

                entity.Property(s => s.PesoOrcamento)
                    .HasColumnName("peso_orcamento")
                    .HasPrecision(5, 2);


                // Obra 1:N Servico
                entity.HasOne(s => s.Obra)
                    .WithMany(o => o.Servicos)
                    .HasForeignKey(s => s.ObraId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // DIARIO
            modelBuilder.Entity<DiarioEntidade>(entity =>
            {
                entity.ToTable("diarios");

                entity.HasKey(d => d.Id);

                entity.Property(d => d.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(d => d.ObraId)
                    .HasColumnName("obra_id")
                    .IsRequired();

                entity.Property(d => d.UsuarioId)
                    .HasColumnName("usuario_id")
                    .IsRequired();

                entity.Property(d => d.NumeroSequencial)
                    .HasColumnName("numero_sequencial")
                    .IsRequired();

                entity.Property(d => d.Data)
                    .HasColumnName("data")
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(d => d.Clima)
                    .HasColumnName("clima")
                    .HasMaxLength(100);

                entity.Property(d => d.EfetivoMaoObra)
                    .HasColumnName("efetivo_mao_obra");

                entity.Property(d => d.Equipamentos)
                    .HasColumnName("equipamentos");

                entity.Property(d => d.Ocorrencias)
                    .HasColumnName("ocorrencias");

                entity.Property(d => d.Comentario)
                    .HasColumnName("comentario");

                entity.Property(d => d.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(d => d.CriadoEm)
                    .HasColumnName("criado_em")
                    .HasDefaultValueSql("SYSDATETIME()")
                    .IsRequired();


                // Obra 1:N Diario
                entity.HasOne(d => d.Obra)
                    .WithMany(o => o.Diarios)
                    .HasForeignKey(d => d.ObraId)
                    .OnDelete(DeleteBehavior.Cascade);


                // Usuario 1:N Diario
                entity.HasOne(d => d.Usuario)
                    .WithMany(u => u.Diarios)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);


                // Um diário por data dentro da obra
                entity.HasIndex(d => new
                {
                    d.ObraId,
                    d.Data
                })
                .IsUnique();


                // Número sequencial único dentro da obra
                entity.HasIndex(d => new
                {
                    d.ObraId,
                    d.NumeroSequencial
                })
                .IsUnique();
            });

            // APONTAMENTO
            modelBuilder.Entity<ApontamentoEntidade>(entity =>
            {
                entity.ToTable("apontamentos");

                entity.HasKey(a => a.Id);

                entity.Property(a => a.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(a => a.DiarioId)
                    .HasColumnName("diario_id")
                    .IsRequired();

                entity.Property(a => a.ServicoId)
                    .HasColumnName("servico_id")
                    .IsRequired();

                entity.Property(a => a.QuantidadeExecutada)
                    .HasColumnName("quantidade_executada")
                    .HasPrecision(12, 2)
                    .IsRequired();


                // Diario 1:N Apontamento
                entity.HasOne(a => a.Diario)
                    .WithMany(d => d.Apontamentos)
                    .HasForeignKey(a => a.DiarioId)
                    .OnDelete(DeleteBehavior.Cascade);


                // Servico 1:N Apontamento
                entity.HasOne(a => a.Servico)
                    .WithMany(s => s.Apontamentos)
                    .HasForeignKey(a => a.ServicoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // HISTORICO_STATUS
            modelBuilder.Entity<HistoricoStatusEntidade>(entity =>
            {
                entity.ToTable("historico_status");

                entity.HasKey(h => h.Id);

                entity.Property(h => h.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(h => h.DiarioId)
                    .HasColumnName("diario_id")
                    .IsRequired();

                entity.Property(h => h.UsuarioId)
                    .HasColumnName("usuario_id")
                    .IsRequired();

                entity.Property(h => h.StatusAnterior)
                    .HasColumnName("status_anterior")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(h => h.StatusNovo)
                    .HasColumnName("status_novo")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(h => h.Comentario)
                    .HasColumnName("comentario");

                entity.Property(h => h.DataHora)
                    .HasColumnName("data_hora")
                    .HasDefaultValueSql("SYSDATETIME()")
                    .IsRequired();


                // Diario 1:N HistoricoStatus
                entity.HasOne(h => h.Diario)
                    .WithMany(d => d.HistoricosStatus)
                    .HasForeignKey(h => h.DiarioId)
                    .OnDelete(DeleteBehavior.Cascade);


                // Usuario 1:N HistoricoStatus
                entity.HasOne(h => h.Usuario)
                    .WithMany(u => u.HistoricosStatus)
                    .HasForeignKey(h => h.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}