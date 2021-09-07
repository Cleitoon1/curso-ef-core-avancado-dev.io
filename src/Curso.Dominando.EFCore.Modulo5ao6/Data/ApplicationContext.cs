using Curso.Dominando.EFCore.Modulo5ao6.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Curso.Dominando.EFCore.Modulo5ao6.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data Source=localhost; Initial Catalog=EFCoreAvancadoDb2; Integrated Security=true; MultipleActiveResultSets=true";
            //optionsBuilder.UseSqlServer(strConnection, _ => _.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            optionsBuilder.UseSqlServer(strConnection)
                .EnableSensitiveDataLogging()
                //.UseLazyLoadingProxies()
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Departamento>().HasQueryFilter(_ => !_.Excluido);
        }
    }
}
