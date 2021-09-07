using Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Data.Configurations
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            //builder.HasOne(_ => _.Governador).WithOne(_ => _.Estado); // SEM PASSAR CHAVE ESTRANGEIRA
            builder.HasOne(_ => _.Governador).WithOne(_ => _.Estado).HasForeignKey<Governador>(_ => _.EstadoReference);

            //builder.HasMany(_ => _.Cidades).WithOne(); // caso na outra classe não tenha entidade de navegação
            builder.HasMany(_ => _.Cidades).WithOne(_ => _.Estado).IsRequired(false); // se o nome do campo for padrão ele ja faz sozinho

            builder.Navigation(_ => _.Governador).AutoInclude();
        }
    }
}
