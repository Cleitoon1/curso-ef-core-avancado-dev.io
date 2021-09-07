using Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Conversores;
using Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Data.Configurations;
using Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Conversor> Conversores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Governador> Governadores { get; set; }
        public DbSet<Ator> Atores { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Instrutor> Instrutores { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Dictionary<string, object>> Configuracoes => Set<Dictionary<string, object>>("Configuracoes");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data Source=localhost; Initial Catalog=EFCoreAvancadoDb2; Integrated Security=true; MultipleActiveResultSets=true";
            optionsBuilder
                .UseSqlServer(strConnection)
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

            //modelBuilder.Entity<Departamento>().Property(_ => _.Descricao).UseCollation("SQL_Latin1_General_CP1_CS_AS");

            //modelBuilder.HasSequence<int>("MinhaSequencia", "sequencias")
            //    .StartsAt(1)
            //    .IncrementsBy(2)
            //    .HasMin(1)
            //    .HasMax(10)
            //    .IsCyclic();

            //modelBuilder.Entity<Departamento>().Property(_ => _.Id).HasDefaultValueSql("NEXT VAlUE FOR sequencias.MinhaSequencia");

            //modelBuilder.Entity<Departamento>()
            //    .HasIndex(_ => new { _.Descricao, _.Ativo })
            //    .HasDatabaseName("IDX_MEU_INDICE_COMPOSTO")
            //    .HasFilter("Descricao is not null")
            //    .HasFillFactor(80)
            //    .IsUnique();

            //modelBuilder.Entity<Estado>().HasData(new[]
            //{
            //    new Estado() { Id = 1, Nome = "São Paulo"},
            //    new Estado() { Id = 2, Nome = "Rio de Janeiro"},
            //});

            //modelBuilder.HasDefaultSchema("cadastros");
            //Microsoft.EntityFrameworkCore.Storage.ValueConversion -- DLL que da acesso a todos os conversores default do ef
            //modelBuilder.Entity<Estado>().ToTable("Estados", "SegundoEsquema");


            //modelBuilder.Entity<Conversor>().Property(_ => _.Versao).HasConversion(_ => _.ToString(), _ => (Versao)Enum.Parse(typeof(Versao), _));
            //var conversao = new ValueConverter<Versao, string>(_ => _.ToString(), _ => (Versao)Enum.Parse(typeof(Versao), _));
            //var conversao1 = new EnumToStringConverter<Versao>();
            //modelBuilder.Entity<Conversor>().Property(_ => _.Versao).HasConversion(conversao1);
            //modelBuilder.Entity<Conversor>().Property(_ => _.Status).HasConversion(new ConversoresCustomizados());

            //modelBuilder.Entity<Departamento>().Property<DateTime>("UltimaAtualizacao");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            modelBuilder.SharedTypeEntity<Dictionary<string, object>>("Configuracoes", _ =>
            {
                _.Property<int>("Id");
                _.Property<string>("Chave").HasColumnType("varchar(40)").IsRequired();
                _.Property<string>("Valor").HasColumnType("varchar(255)").IsRequired();
            });
        }
    }
}
