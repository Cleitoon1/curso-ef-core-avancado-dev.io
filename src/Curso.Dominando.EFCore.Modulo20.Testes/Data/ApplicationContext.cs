using Curso.Dominando.EFCore.Modulo20.Testes.Entities;
using Microsoft.EntityFrameworkCore;

namespace Curso.Dominando.EFCore.Modulo20.Testes.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
