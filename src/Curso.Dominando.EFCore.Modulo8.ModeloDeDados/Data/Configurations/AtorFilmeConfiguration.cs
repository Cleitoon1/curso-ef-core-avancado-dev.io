using Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Data.Configurations
{
    public class AtorFilmeConfiguration : IEntityTypeConfiguration<Ator>
    {
        public void Configure(EntityTypeBuilder<Ator> builder)
        {
            //builder.HasMany(_ => _.Filmes).WithMany(_ => _.Atores);
            //builder.HasMany(_ => _.Filmes).WithMany(_ => _.Atores).UsingEntity(_ => _.ToTable("AtoresFilmes"));
            builder.HasMany(_ => _.Filmes).WithMany(_ => _.Atores).UsingEntity<Dictionary<string, object>>(
                "FilmesAtores",
                _ => _.HasOne<Filme>().WithMany().HasForeignKey("FilmeId"),
                _ => _.HasOne<Ator>().WithMany().HasForeignKey("AtorId"),
                _ => 
                {
                    _.Property<DateTime>("CadastradoEm").HasDefaultValueSql("GETDATE()");
                }
            );
        }
    }
}
