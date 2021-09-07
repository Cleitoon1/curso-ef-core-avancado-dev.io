using Curso.Dominando.EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Curso.Dominando.EFCore.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data Source=localhost; Initial Catalog=EFCoreAvancadoDb; Integrated Security=true; MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(strConnection)
                .EnableSensitiveDataLogging()
                .UseLazyLoadingProxies()
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
