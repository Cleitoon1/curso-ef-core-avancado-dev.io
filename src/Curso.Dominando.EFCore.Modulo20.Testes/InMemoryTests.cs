using Curso.Dominando.EFCore.Modulo20.Testes.Data;
using Curso.Dominando.EFCore.Modulo20.Testes.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace Curso.Dominando.EFCore.Modulo20.Testes
{
    public class InMemoryTests
    {
        [Fact]
        public void Deve_inserir_um_departamento()
        {
            var departamento = new Departamento
            {
                Descricao = "Tecnologia",
                CadastradoEm = DateTime.Now
            };

            var context = CreateContext();
            context.Departamentos.Add(departamento);
            var inseridos = context.SaveChanges();
            Assert.Equal(1, inseridos);
        }

        [Fact]
        public void Nao_implemtando_funcoes_de_datas_para_o_provider_inmemory()
        {
            var departamento = new Departamento
            {
                Descricao = "Tecnologia",
                CadastradoEm = DateTime.Now
            };

            var context = CreateContext();
            context.Departamentos.Add(departamento);
            var inseridos = context.SaveChanges();

            Action action = () => context.Departamentos.FirstOrDefault(_ => EF.Functions.DateDiffDay(DateTime.Now, _.CadastradoEm) > 0);
            Assert.Throws<InvalidOperationException>(action);
        }

        private ApplicationContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase("InMemoryTest").Options;
            return new ApplicationContext(options);
        }
    }
}
