using Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Data.Configurations
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            //builder.ToTable("Pessoa")
            //    .HasDiscriminator<int>("TipoPessoa")
            //    .HasValue<Pessoa>(3)
            //    .HasValue<Instrutor>(6)
            //    .HasValue<Aluno>(99);
            builder.ToTable("Pessoa");
        }
    }

    public class InstrutorConfiguration : IEntityTypeConfiguration<Instrutor>
    {
        public void Configure(EntityTypeBuilder<Instrutor> builder)
        {
            builder.ToTable("Instrutores");
        }
    }

    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            //builder.ToTable("Pessoa")
            //    .HasDiscriminator<int>("TipoPessoa")
            //    .HasValue<Pessoa>(3)
            //    .HasValue<Instrutor>(6)
            //    .HasValue<Aluno>(99);
            builder.ToTable("Alunos");
        }
    }
}
