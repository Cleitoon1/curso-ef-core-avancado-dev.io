using Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Data.Configurations
{
    public class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            //builder.Property(_ => _.CPF).HasField("_cpf");
            //builder.Property("_cpf");
            builder.Property("_cpf").HasColumnName("cpf").HasMaxLength(11);
        }
    }
}
