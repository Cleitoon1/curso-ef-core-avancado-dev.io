using Curso.Dominando.EFCore.Modulo2122.ComportamentosEDiagnostics.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;

namespace Curso.Dominando.EFCore.Modulo2122.ComportamentosEDiagnostics.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data Source=localhost; Initial Catalog=sobreescrevendo_comportamentos; Integrated Security=true; MultipleActiveResultSets=true";
            optionsBuilder
                .UseSqlServer(strConnection)
                //.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                //.ReplaceService<IQuerySqlGeneratorFactory, MySqlServerQuerySqlGeneratorFactory>()
                .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }
    }
}