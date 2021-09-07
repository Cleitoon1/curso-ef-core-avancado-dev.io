using Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.OwnsOne(_ => _.Endereco, end =>
            {
                end.Property(p => p.Logradouro).HasColumnName("Logradouro");
                end.Property(p => p.Bairro).HasColumnName("Bairro");
                end.Property(p => p.Cidade).HasColumnName("Cidade");
                end.Property(p => p.Estado).HasColumnName("Estado");
                end.ToTable("Enderecos");
            });

        }
    }
}
