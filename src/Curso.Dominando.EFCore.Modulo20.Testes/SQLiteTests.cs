using Curso.Dominando.EFCore.Modulo20.Testes.Data;
using Curso.Dominando.EFCore.Modulo20.Testes.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace Curso.Dominando.EFCore.Modulo20.Testes
{
    public class SQLiteTests
    {
        [Theory]
        [InlineData("Tecnologia")]
        [InlineData("Financeiro")]
        [InlineData("Departamento Pessoal")]
        public void Deve_inserir_e_consultar_um_departamento(string descricao)
        {
            var departamento = new Departamento
            {
                Descricao = descricao,
                CadastradoEm = DateTime.Now
            };

            var context = CreateContext();
            context.Database.EnsureCreated();
            context.Departamentos.Add(departamento);
            var inseridos = context.SaveChanges();
            var departamentos = context.Departamentos.FirstOrDefault(_ => _.Descricao == descricao);
            Assert.Equal(1, inseridos);
            Assert.Equal(descricao, departamentos.Descricao);
        }

        private static ApplicationContext CreateContext()
        {
            var conexao = new SqliteConnection("Datasource=:memory:");
            conexao.Open();
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseSqlite(conexao).Options;
            return new ApplicationContext(options);
        }
    }
}
