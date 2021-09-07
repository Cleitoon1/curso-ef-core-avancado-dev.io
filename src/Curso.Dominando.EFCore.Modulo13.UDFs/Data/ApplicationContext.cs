using Curso.Dominando.EFCore.Modulo13.UDFs.Domain;
using Curso.Dominando.EFCore.Modulo13.UDFs.Funcoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Linq;
using System.Reflection;

namespace Curso.Dominando.EFCore.Modulo13.UDFs.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }

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
            //MinhasFuncoes.RegistrarFuncoes(modelBuilder);

            modelBuilder.HasDbFunction(_minhaFuncao).HasName("LEFT").IsBuiltIn();
            modelBuilder.HasDbFunction(_letrasMaiusculas).HasName("ConverterParaLetrasMaiusculas").HasSchema("dbo");
            modelBuilder.HasDbFunction(_dateDiff).HasName("DATEDIFF").HasTranslation(_ =>
            {
                var argumentos = _.ToList();
                var constante = (SqlConstantExpression)argumentos[0];
                argumentos[0] = new SqlFragmentExpression(constante.Value.ToString());
                return new SqlFunctionExpression("DATEDIFF", argumentos, false, new[] { false, false, false }, typeof(int), null);
            }).IsBuiltIn();
        }

        private static MethodInfo _minhaFuncao = typeof(MinhasFuncoes).GetRuntimeMethod(nameof(MinhasFuncoes.Left), new[] { typeof(string), typeof(int) });
        private static MethodInfo _letrasMaiusculas = typeof(MinhasFuncoes).GetRuntimeMethod(nameof(MinhasFuncoes.LetrasMaiusculas), new[] { typeof(string) });
        private static MethodInfo _dateDiff = typeof(MinhasFuncoes).GetRuntimeMethod(nameof(MinhasFuncoes.DateDiff), new[] { typeof(string), typeof(DateTime), typeof(DateTime) });
    }
}