using Curso.Dominando.EFCore.Modulo19.DicasETruques.Data;
using Curso.Dominando.EFCore.Modulo19.DicasETruques.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Curso.Dominando.EFCore.Modulo19.DicasETruques
{
    class Program
    {
        static void Main(string[] args)
        {
            //ToQueryString();
            //DebugView();
            //Clear();
            //ConsultaFiltrada();
            //SingleOrDefaultVsFirstOrDefault();
            //SemChavePrimaria();
            //ToView();
            //NaoUnicode();
            //OperadoresDeAgregacao();
            //OperadoresDeAgregacaoNoAgrupamento();
            ContadorDeEventos();
        }

        static void ContadorDeEventos()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Console.WriteLine($" PID: {System.Diagnostics.Process.GetCurrentProcess().Id}");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                var departamento = new Departamento
                {
                    Descricao = $"Departamento Sem Colaborador"
                };

                db.Departamentos.Add(departamento);
                db.SaveChanges();

                _ = db.Departamentos.Find(1);
                _ = db.Departamentos.AsNoTracking().FirstOrDefault();
            }

        }

        static void OperadoresDeAgregacaoNoAgrupamento()
        {
            using var db = new ApplicationContext();

            var sql = db.Departamentos
                .GroupBy(p => p.Descricao)
                .Where(p => p.Count() > 1)
                .Select(p =>
                    new
                    {
                        Descricao = p.Key,
                        Contador = p.Count()
                    }).ToQueryString();

            Console.WriteLine(sql);
        }

        static void OperadoresDeAgregacao()
        {
            using var db = new ApplicationContext();

            var sql = db.Departamentos
                .GroupBy(p => p.Descricao)
                .Select(p =>
                    new
                    {
                        Descricao = p.Key,
                        Contador = p.Count(),
                        Media = p.Average(p => p.Id),
                        Maximo = p.Max(p => p.Id),
                        Soma = p.Sum(p => p.Id)
                    }).ToQueryString();

            Console.WriteLine(sql);
        }

        static void NaoUnicode()
        {
            using var db = new ApplicationContext();
            var sql = db.Database.GenerateCreateScript();
            Console.WriteLine(sql);
        }

        static void ToView()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Database.ExecuteSqlRaw(@"
                CREATE VIEW vw_departamento_relatorio AS
                SELECT d.Descricao, count(c.id) as Colaboradores
                FROM Departamentos d
                LEFT JOIN Colaboradores c ON c.DepartamentoId = d.Id
                GROUP BY D.Descricao");

            db.Departamentos.AddRange(Enumerable.Range(1, 10).Select(_ => new Departamento
            {
                Descricao = $"Departamento {_}",
                Colaboradores = Enumerable.Range(1, 10).Select(__ => new Colaborador
                {
                    Nome = $"Colaborador {__}"
                }).ToList()
            }));

            db.Departamentos.Add(new Departamento { Descricao = $"Departamento sem colaborador" });
            db.SaveChanges();

            var relatorio = db.DepartamentoRelatorios.Where(_ => _.Colaboradores < 20).OrderBy(_ => _.Departamento).ToList();
            foreach (var item in relatorio)
                Console.WriteLine($"{item.Departamento} - Colaboradores: {item.Colaboradores}");
        }

        static void SemChavePrimaria()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var usuariosFuncoes = db.UsuarioFuncoes.Where(_ => _.UsuarioId == Guid.NewGuid()).ToArray();
        }

        static void SingleOrDefaultVsFirstOrDefault()
        {
            using var db = new ApplicationContext();
            Console.WriteLine("SingleOrDefault: ");
            _ = db.Departamentos.SingleOrDefault(_ => _.Id > 2);
            Console.WriteLine("FirstOrDefault: ");
            _ = db.Departamentos.FirstOrDefault(_ => _.Id > 2);
        }

        static void ConsultaFiltrada()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();

            var sql = db.Departamentos.Include(_ => _.Colaboradores.Where(c => c.Nome.Contains("teste"))).ToQueryString();
            Console.WriteLine(sql);
        }

        static void Clear()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();

            db.Departamentos.Add(new Domain.Departamento { Descricao = "teste debug view" });
            db.ChangeTracker.Clear();
        }

        static void DebugView()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();

            db.Departamentos.Add(new Domain.Departamento { Descricao = "teste debug view" });
            var query = db.Departamentos.Where(_ => _.Id > 2);
        }

        static void ToQueryString()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();

            var query = db.Departamentos.Where(_ => _.Id > 2);
            var sql = query.ToQueryString();
            Console.WriteLine(sql);
        }
    }
}
