using Curso.Dominando.EFCore.Modulo9.DataAnnotations.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Curso.Dominando.EFCore.Modulo9.DataAnnotations.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Atributo> Atributos { get; set; }
        public DbSet<Voo> Voos { get; set; }
        public DbSet<Aeroporto> Aeroportos { get; set; }

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

        }
    }
}
