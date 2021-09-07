using Curso.Dominando.EFCore.Modulo10.EFFunctions.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Curso.Dominando.EFCore.Modulo10.EFFunctions.Data
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
                .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funcao>(_ => _.Property<string>("PropriedadeSombra").HasColumnType("VARCHAR(100)").HasDefaultValueSql("'teste'"));
        }
    }
}