using Curso.Dominando.EFCore.Modulo7Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Curso.Dominando.EFCore.Modulo7Infraestrutura
{
    class Program
    {
        static void Main(string[] args)
        {
            //ConsultarDepartamentos();
            //DadosSensiveis();
            //HabilitandoBatchSize();
            //TempoComandoGeral();
            TempoComandoGeralFluxo();
        }

        static void TempoComandoGeralFluxo()
        {
            using var db = new ApplicationContext();
            db.Database.SetCommandTimeout(10);
            db.Database.ExecuteSqlRaw("WAITFOR DELAY '00:00:07';SELECT 1");
        }

        static void TempoComandoGeral()
        {
            using var db = new ApplicationContext();
            db.Database.ExecuteSqlRaw("WAITFOR DELAY '00:00:07';SELECT 1");
        }

        static void HabilitandoBatchSize()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            for (int i = 0; i < 50; i++)
                db.Departamentos.Add(new Domain.Departamento() { Descricao = $"Departamento {i}" });

            db.SaveChanges();            
        }

        static void DadosSensiveis()
        {
            using var db = new ApplicationContext();
            var departamentos = db.Departamentos.Where(_ => _.Descricao == "Departamento").ToArray();
        }

        static void ConsultarDepartamentos()
        {
            using var db = new ApplicationContext();
            var departamentos = db.Departamentos.Where(_ => _.Id > 0).ToArray();
        }


    }
}
