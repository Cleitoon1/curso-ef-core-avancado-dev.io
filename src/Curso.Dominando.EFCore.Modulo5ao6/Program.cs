using Curso.Dominando.EFCore.Modulo5ao6.Data;
using Curso.Dominando.EFCore.Modulo5ao6.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Curso.Dominando.EFCore.Modulo5ao6
{
    class Program
    {
        static void Main(string[] args)
        {
            //FiltroGlobal();
            //IgnorarFiltroGlobal();
            //ConsultaProjetada();
            //ConsultaParametrizada();
            //ConsultaInterpolada();
            //ConsultaComTag();
            //EntendendoConsultas1NN1();
            //DivisaoDeConsultas();
            //CriarStoredProcedure();
            //InserirDadosViaProcedure();
            //CriarStoredProcedureDeConsulta();
            ConsultarDadosViaProcedure();
        }

        static void ConsultarDadosViaProcedure()
        {
            var dep = new SqlParameter("@dep", "proc");
            using var db = new ApplicationContext();
            //var deparamentos = db.Departamentos.FromSqlRaw("execute GetDepartamento @dep", dep).ToList();
            var deparamentos = db.Departamentos.FromSqlInterpolated($"execute GetDepartamento {dep}").ToList();
            foreach (var deparamento in deparamentos)
                Console.WriteLine(deparamento);
        }

        static void CriarStoredProcedureDeConsulta()
        {
            var getDepartamento = @"
            CREATE OR ALTER PROCEDURE GetDepartamento
                @Descricao VARCHAR(50)
            AS
            BEGIN
                SELECT * FROM Departamentos where Descricao like @Descricao + '%'
            END
            ";
            using var db = new ApplicationContext();
            db.Database.ExecuteSqlRaw(getDepartamento);
        }

        static void InserirDadosViaProcedure()
        {
            using var db = new ApplicationContext();
            db.Database.ExecuteSqlRaw("execute CriarDepartamento @p0, @p1", "Procedures", 1);
        }

        static void CriarStoredProcedure()
        {
            var criarDepartamento = @"
            CREATE OR ALTER PROCEDURE CriarDepartamento
                @Descricao VARCHAR(50),
                @Ativo bit
            AS
            BEGIN
                INSERT INTO
                    Departamentos(Descricao, Ativo, Excluido)
                VALUES(@Descricao, @Ativo, 0)
            END
            ";
            using var db = new ApplicationContext();
            db.Database.ExecuteSqlRaw(criarDepartamento);
        }

        static void DivisaoDeConsultas()
        {
            using var db = new ApplicationContext();
            Setup(db);

            var departamentos = db.Departamentos.Include(_ => _.Funcionarios).AsSingleQuery().Where(_ => _.Id > 3).ToList();
            foreach (var deparamento in departamentos)
                Console.WriteLine(deparamento);
        }

        static void EntendendoConsultas1NN1()
        {
            using var db = new ApplicationContext();
            Setup(db);

            //1xN realiza left join (não necessariamente precisa ter registros relacionados)
            var departamentos = db.Departamentos.Include(_ => _.Funcionarios).ToList();
            foreach (var deparamento in departamentos)
                Console.WriteLine(deparamento);


            //Nx1 - realiza inner join (precisa ter registros relacionados)
            var funcionarios = db.Funcionarios.Include(_ => _.Departamento).ToList();
            foreach (var funcionario in funcionarios)
                Console.WriteLine(funcionario);

        }

        static void ConsultaComTag()
        {
            using var db = new ApplicationContext();
            Setup(db);

            var departamentos = db.Departamentos.Where(_ => !_.Excluido).TagWith("comentário de teste para o servidor").ToList();
            foreach (var deparamento in departamentos)
                Console.WriteLine(deparamento);
        }

        static void ConsultaInterpolada()
        {
            using var db = new ApplicationContext();
            Setup(db);
        
            var departamentos = db.Departamentos.FromSqlInterpolated($"SELECT * FROM Departamentos WITH(NOLOCK) where id > {1}")
                .Where(_ => !_.Excluido).ToList();
            foreach (var deparamento in departamentos)
                Console.WriteLine(deparamento);
        }

        static void ConsultaParametrizada()
        {
            using var db = new ApplicationContext();
            Setup(db);
            var id = new SqlParameter
            {
                Value = 1,
                SqlDbType = System.Data.SqlDbType.Int
            };
            var departamentos = db.Departamentos.FromSqlRaw("SELECT * FROM Departamentos WITH(NOLOCK) where id > {0}", id)
                .Where(_ => !_.Excluido).ToList();
            foreach (var deparamento in departamentos)
                Console.WriteLine(deparamento);
        }

        static void ConsultaProjetada()
        {
            using var db = new ApplicationContext();
            Setup(db);

            var departamentos = db.Departamentos.Where(_ => _.Id > 0).Select(_ => new { _.Descricao, Funcionarios = _.Funcionarios.Select(s => s.Nome)}).ToList();
            foreach (var deparamento in departamentos)
                Console.WriteLine($"Departamento - Descrição: {deparamento.Descricao}, Funcionários: {string.Join(',', deparamento.Funcionarios)}");
        }

        static void IgnorarFiltroGlobal()
        {
            using var db = new ApplicationContext();
            Setup(db);

            var departamentos = db.Departamentos.IgnoreQueryFilters().Where(_ => _.Id > 0).ToList();
            foreach (var deparamento in departamentos)
                Console.WriteLine(deparamento);
        }

        static void FiltroGlobal()
        {
            using var db = new ApplicationContext();
            Setup(db);

            var departamentos = db.Departamentos.Where(_ => _.Id > 0).ToList();
            foreach (var deparamento in departamentos)
                Console.WriteLine(deparamento);
        }

        static void Setup(ApplicationContext db)
        {
            if (!db.Database.EnsureCreated() || db.Departamentos.Any())
                return;

            db.Departamentos.AddRange(new Departamento
            {
                Ativo = true,
                Excluido = true,
                Descricao = "Departamento 01",
                Funcionarios = new List<Funcionario>
                {
                    new Funcionario("Cleiton Alves", "11122233344", "123456")
                }
            }, new Departamento
            {
                Ativo = true,
                Excluido = false,
                Descricao = "Departamento 02",
                Funcionarios = new List<Funcionario>
                {
                    new Funcionario("Fernanda Lariane Alves", "11122233344", "123456")
                }
            });
            db.SaveChanges();
            db.ChangeTracker.Clear();
        }
    }
}
