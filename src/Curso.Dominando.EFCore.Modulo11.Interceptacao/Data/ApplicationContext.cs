using Curso.Dominando.EFCore.Modulo11.Interceptacao.Domain;
using Curso.Dominando.EFCore.Modulo11.Interceptacao.Interceptadores;
using Microsoft.EntityFrameworkCore;
using System;

namespace Curso.Dominando.EFCore.Modulo11.Interceptacao.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Funcao> Funcoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data Source=localhost; Initial Catalog=EFCoreAvancadoDb2; Integrated Security=true; MultipleActiveResultSets=true";
            optionsBuilder
                .UseSqlServer(strConnection)
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .AddInterceptors(new InterceptacaoDeComandos(), new InterceptadorDeConexao(), new InterceptadorPersistencia())
                .EnableSensitiveDataLogging();

           
            base.OnConfiguring(optionsBuilder);
            
        }
    }
}