using Curso.Dominando.EFCore.Modulo16.OutrosBDs.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Curso.Dominando.EFCore.Modulo16.OutrosBDs.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //const string strConnection = "Data Source=localhost; Initial Catalog=EFCoreAvancadoDb2; Integrated Security=true; MultipleActiveResultSets=true";
            //const string strConnectionPg = "Host=localhost;Database=DEVIO04;Username=postgres;Password=123";
            optionsBuilder
                //.UseSqlServer(strConnection)
                //.UseNpgsql(strConnectionPg)
                //.UseSqlite("Data source=devio04.db")
                //.UseInMemoryDatabase(databaseName: "te,ste-devio04")
                .UseCosmos("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", "devio-04")
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>(_ =>
            {
                _.HasKey(c => c.Id);

                _.ToContainer("Pessoas"); //configuração para o cosmos
                //_.Property(p => p.Nome).HasMaxLength(60).IsUnicode(false);
            });
        }
    }
}