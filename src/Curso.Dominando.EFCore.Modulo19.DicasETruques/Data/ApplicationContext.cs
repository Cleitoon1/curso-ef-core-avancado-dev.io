using Curso.Dominando.EFCore.Modulo19.DicasETruques.Domain;
using Curso.Dominando.EFCore.Modulo19.DicasETruques.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Curso.Dominando.EFCore.Modulo19.DicasETruques.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<UsuarioFuncao> UsuarioFuncoes { get; set; }
        public DbSet<DepartamentoRelatorio> DepartamentoRelatorios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data Source=localhost; Initial Catalog=EFCoreAvancadoTips; Integrated Security=true; MultipleActiveResultSets=true";
            optionsBuilder
                .UseSqlServer(strConnection)
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartamentoRelatorio>(_ =>
            {
                _.HasNoKey();
                _.ToView("vw_departamento_relatorio");
                _.Property(p => p.Departamento).HasColumnName("Descricao");
            });

            modelBuilder.Entity<UsuarioFuncao>().HasNoKey();

            var properties = modelBuilder.Model.GetEntityTypes()
                .SelectMany(_ => _.GetProperties())
                    .Where(_ => _.ClrType == typeof(string) && _.GetColumnType() == null);
            foreach (var item in properties)
                item.SetIsUnicode(false);

            modelBuilder.ToSnakeCaseNames();
        }
    }
}