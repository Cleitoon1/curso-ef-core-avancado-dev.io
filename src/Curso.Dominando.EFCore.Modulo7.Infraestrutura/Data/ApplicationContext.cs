using Curso.Dominando.EFCore.Modulo7Infraestrutura.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Curso.Dominando.EFCore.Modulo7Infraestrutura.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly StreamWriter writer = new StreamWriter("log_do_ef_core.txt", append: true);
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data Source=localhost; Initial Catalog=EFCoreAvancadoDb2; Integrated Security=true; MultipleActiveResultSets=true";
            optionsBuilder
                .UseSqlServer(strConnection, _ => _.MaxBatchSize(100).CommandTimeout(160).EnableRetryOnFailure(4, TimeSpan.FromSeconds(20), null))
                //.EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            //.LogTo(Console.WriteLine, new[] { CoreEventId.ContextInitialized, RelationalEventId.CommandExecuted },
            //    Microsoft.Extensions.Logging.LogLevel.Information, DbContextLoggerOptions.LocalTime | DbContextLoggerOptions.SingleLine);
            //.LogTo(writer.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            //.EnableDetailedErrors();
            base.OnConfiguring(optionsBuilder);
        }

        public override void Dispose()
        {
            writer.Flush();
            base.Dispose();
        }
    }
}
