using Curso.Dominando.EFCore.Modulo17.MultiTenant.Domain;
using Curso.Dominando.EFCore.Modulo17.MultiTenant.Provider;
using Microsoft.EntityFrameworkCore;

namespace Curso.Dominando.EFCore.Modulo17.MultiTenant.Data
{
    public class ApplicationContext : DbContext
    {
        //public TenantData TenantData;
        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options/*, TenantData tenantData*/) : base(options)
        {
            //TenantData = tenantData;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(TenantData.TenantId);
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Name = "Person 1", TenantId = "tenant-1"},
                new Person { Id = 2, Name = "Person 2", TenantId = "tenant-1"},
                new Person { Id = 3, Name = "Person 3", TenantId = "tenant-2"}
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Description = "Product 1", TenantId = "tenant-2" },
                new Product { Id = 2, Description = "Product 2", TenantId = "tenant-2" },
                new Product { Id = 3, Description = "Product 3", TenantId = "tenant-1" }
            );

            //modelBuilder.Entity<Person>().HasQueryFilter(_ => _.TenantId == TenantData.TenantId);
            //modelBuilder.Entity<Product>().HasQueryFilter(_ => _.TenantId == TenantData.TenantId);
        }
    }
}
