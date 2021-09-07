using Curso.Dominando.EFCore.Data;
using Curso.Dominando.EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Curso.Dominando.EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //EnsureCreatedAndDeleted();
            //GapDoEnsureCreated();
            //HeathCheckBancoDeDados();

            //warmup
            //new ApplicationContext().Departamentos.AsNoTracking().Any();
            //GerenciarEstadoConexao(false);
            //GerenciarEstadoConexao(true);

            //ExecuteSQL();
            //SQLInjection();

            //MigracoesPendentes();
            //AplicarMigracaoEmTempoExecucao();
            //TodasMigracoesAplicadas();

            //ScriptGeralDoBancoDeDados();

            //CarregamentoAdiantado();
            //CarregamentoExplicito();
            CarregamentoLento();
        }

        static void CarregamentoLento()
        {
            using var db = new ApplicationContext();
            SetupTiposCarregamentos(db);
            //db.ChangeTracker.LazyLoadingEnabled = false;

            var departamentos = db.Departamentos;
            foreach (var departamento in departamentos)
                Console.WriteLine(departamento);
        }

        static void CarregamentoExplicito()
        {
            using var db = new ApplicationContext();
            SetupTiposCarregamentos(db);
            var departamentos = db.Departamentos.ToList();
            foreach (var departamento in departamentos)
            {
                if (departamento.Id == 2)
                    //db.Entry(departamento).Collection(_ => _.Funcionarios).Load();
                    db.Entry(departamento).Collection(_ => _.Funcionarios).Query().Where(_ => _.Id >= 2).ToList();
                Console.WriteLine(departamento);
            }
        }

        static void CarregamentoAdiantado()
        {
            using var db = new ApplicationContext();
            SetupTiposCarregamentos(db);
            var departamentos = db.Departamentos.Include(_ => _.Funcionarios);
            foreach (var departamento in departamentos)
                Console.WriteLine(departamento);
        }

        static void SetupTiposCarregamentos(ApplicationContext db)
        {
            if (db.Departamentos.Any())
                return;

            db.Departamentos.AddRange(new Departamento
            {
                Descricao = "Departamento 01",
                Funcionarios = new List<Funcionario>
                {
                    new Funcionario("Cleiton Alves", "11122233344", "123456")
                }
            }, new Departamento
            {
                Descricao = "Departamento 02",
                Funcionarios = new List<Funcionario>
                {
                    new Funcionario("Fernanda Lariane Alves", "11122233344", "123456")
                }
            });
            db.SaveChanges();
            db.ChangeTracker.Clear();
        }

        static void ScriptGeralDoBancoDeDados()
        {
            using var db = new ApplicationContext();
            var script = db.Database.GenerateCreateScript();
            Console.WriteLine(script);
        }

        static void TodasMigracoesAplicadas()
        {
            using var db = new ApplicationContext();

            var migracoes = db.Database.GetAppliedMigrations();

            Console.WriteLine($"Total de Migrações aplicacadas: {migracoes.Count()}");
            foreach (var item in migracoes)
                Console.WriteLine($"Migração: {item}");
        }

        static void TodasMigracoes()
        {
            using var db = new ApplicationContext();

            var migracoes = db.Database.GetMigrations();

            Console.WriteLine($"Total de Migrações: {migracoes.Count()}");
            foreach (var item in migracoes)
                Console.WriteLine($"Migração: {item}");
        }

        static void AplicarMigracaoEmTempoExecucao()
        {
            using var db = new ApplicationContext();
            db.Database.Migrate();
        }

        static void MigracoesPendentes()
        {
            using var db = new ApplicationContext();

            var migracoesPendentes = db.Database.GetPendingMigrations();

            Console.WriteLine($"Total de Migrações pendentes: {migracoesPendentes.Count()}");
            foreach (var item in migracoesPendentes)
                Console.WriteLine($"Migração: {item}");
        }

        static void SQLInjection()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Departamentos.AddRange(new Departamento
            {
                Descricao = "Departamento 01"
            }, new Departamento
            {
                Descricao = "Departamento 02"
            });
            db.SaveChanges();

            var descricao = "Teste 'or 1='1";
            db.Database.ExecuteSqlRaw($"update departamentos set descricao = 'AtaqueSQLInjection' where descricao = '{descricao}'");
            foreach (var item in db.Departamentos.AsNoTracking())
                Console.WriteLine(item.ToString());
        }

        static void ExecuteSQL()
        {
            //Primeira opção
            using var db = new ApplicationContext();
            db.Database.OpenConnection();
            using (var cmd = db.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "SELECT 1";
                cmd.ExecuteNonQuery();
            }

            //segunda opção
            var descricao = "descricao teste";
            db.Database.ExecuteSqlRaw("UPDATE Departamentos set descricao = {0} where id = 1", descricao);

            //terceira opção
            db.Database.ExecuteSqlInterpolated($"UPDATE departamentos set descricao = {descricao} where id = 1");
        }

        static int _count;
        static void GerenciarEstadoConexao(bool gerenciarEstadoConexao)
        {
            using var db = new ApplicationContext();
            var time = Stopwatch.StartNew();

            var conexao = db.Database.GetDbConnection();
            _count = 0;
            conexao.StateChange += (_, __) => ++_count;
            if (gerenciarEstadoConexao)
                conexao.Open();

            for (int i = 0; i < 200; i++)            
                db.Departamentos.AsNoTracking().Any();
            
            time.Stop();
            var mensagem = $"Tempo: {time.Elapsed}, Gerenciar Estado da conexão: {gerenciarEstadoConexao}, Quantidade alterações conexões: {_count}";
            Console.WriteLine(mensagem);
        }

        static void HeathCheckBancoDeDados()
        {
            using var db = new ApplicationContext();
            
            var conConnect = db.Database.CanConnect();
            if(conConnect)
                Console.WriteLine("Posso me conectar");
            else
                Console.WriteLine("Não posso me conectar");

        }

        static void EnsureCreatedAndDeleted(bool create = true)
        {
            using var db = new ApplicationContext();
            if(create)
                db.Database.EnsureCreated();
            else
                db.Database.EnsureDeleted();
        }

        static void GapDoEnsureCreated()
        {
            //ele não executa o ensucreCreated do db2
            using var db1 = new ApplicationContext();
            using var db2 = new ApplicationContextCidade();
            db1.Database.EnsureCreated();
            db2.Database.EnsureCreated();

            //forma de resolver, "forçando" a criação das tabelas, assim resolvendo o GAP
            var databaseCreator = db2.GetService<IRelationalDatabaseCreator>();
            databaseCreator.CreateTables();
        }
    }
}
