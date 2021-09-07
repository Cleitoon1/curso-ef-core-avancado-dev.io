using Curso.Dominando.EFCore.Modulo18.UOWRepository.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.Dominando.EFCore.Modulo18.UOWRepository.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
