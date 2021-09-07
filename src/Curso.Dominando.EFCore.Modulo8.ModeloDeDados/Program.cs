using Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Data;
using Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados
{
    class Program
    {
        static void Main(string[] args)
        {
            //Collations();
            //PropagarDados();
            //Esquema();
            //ConversorDeValor();
            //ConversorDeValorCustomizado();
            //PropriedadesDeSombra();
            //TrabalhandoComPropriedadesDeSombra();
            //TiposDePropriedades();
            //Relacionamento1x1();
            //Relacionamento1xN();
            //RelacionamentoNxM();
            //CamposDeApoio();
            //ExemploTPH();
            PacotesPropriedades();
        }

        static void PacotesPropriedades()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var configuracao = new Dictionary<string, object>
                {
                    ["Chave"] = "SenhaBancoDeDados",
                    ["Valor"] = Guid.NewGuid().ToString()
                };
                db.Configuracoes.Add(configuracao);
                db.SaveChanges();

                var configuracoes = db.Configuracoes.AsNoTracking().Where(_ => _["Chave"] == "SenhaBancoDeDados");
                foreach (var c in configuracoes)
                    Console.WriteLine($"Chave={c["Chave"]}, Valor={c["Valor"]}");
            }
        }

        static void ExemploTPH()
        {
            using( var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var pessoa = new Pessoa() { Nome = "Fulano de Tal" };
                var instrutor = new Instrutor() { Nome = "Intrutor de Tal" };
                var aluno = new Aluno() { Nome = "Aluno de Tal" };
                db.AddRange(pessoa, aluno, instrutor);
                db.SaveChanges();

                var pessoas = db.Pessoas.AsNoTracking().ToList();
                var instrutores = db.Instrutores.AsNoTracking().ToList();
                //var alunos = db.Alunos.AsNoTracking().ToList();
                var alunos = db.Alunos.OfType<Aluno>().AsNoTracking().ToList();
                Console.WriteLine("Pessoas");
                foreach (var p in pessoas)
                    Console.WriteLine($"Pessoa: Id={p.Id}, Nome={p.Nome}");

                Console.WriteLine("Instrutores");
                foreach (var i in instrutores)
                    Console.WriteLine($"Instrutor: Id={i.Id}, Nome={i.Nome}, Desde={i.DesdeDe}, Tecnologia= {i.Tecnologia}");

                Console.WriteLine("Alunos");
                foreach (var a in alunos)
                    Console.WriteLine($"Aluno: Id={a.Id}, Nome={a.Nome}, DataContrato={a.DataContrato}, Idade={a.Idade}");
            }
        }

        static void CamposDeApoio()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var documento = new Documento();
            documento.SetCpf("12345678901");
            db.Documentos.Add(documento);
            db.SaveChanges();

            foreach (var doc in db.Documentos.AsNoTracking())
                Console.WriteLine($"CPF: {doc.GetCpf()}, {doc.CPF}");            
        }


        static void RelacionamentoNxM()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var ator1 = new Ator { Nome = "Cleiton" };
            var ator2 = new Ator { Nome = "Gladson" };
            var ator3 = new Ator { Nome = "Grace Kelly" };

            var filme1 = new Filme { Nome = "Filme 1" };
            var filme2 = new Filme { Nome = "Filme 2" };
            var filme3 = new Filme { Nome = "Filme 3" };

            ator1.Filmes.Add(filme1);
            ator1.Filmes.Add(filme2);

            ator2.Filmes.Add(filme1);

            ator3.Filmes.Add(filme1);
            ator3.Filmes.Add(filme2);
            ator3.Filmes.Add(filme3);

            db.AddRange(ator1, ator2, ator3);
            db.SaveChanges();

            foreach (var ator in db.Atores.AsNoTracking().Include(_ => _.Filmes).ToList())
            {
                Console.WriteLine($"Ator: Id={ator.Id}, Nome: {ator.Nome}");
                foreach (var filme in ator.Filmes)
                    Console.WriteLine($"Filme: Id={filme.Id}, Nome: {filme.Nome}");
            }
        }

        static void Relacionamento1xN()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var estado = new Estado { Nome = "SP", Governador = new Governador { Nome = "Cleiton" } };
            estado.Cidades.Add(new Cidade { Nome = "São José dos Campos" });
            db.Estados.Add(estado);
            db.SaveChanges();

            db.Estados.AsNoTracking().Include(_ => _.Cidades).ToList().ForEach(_ => Console.WriteLine(_));
        }

        static void Relacionamento1x1()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Estados.Add(new Estado { Nome = "SP", Governador = new Governador { Nome = "Cleiton" } });
            db.SaveChanges();

            db.Estados.AsNoTracking().ToList().ForEach(_ => Console.WriteLine(_));
        }

        static void TiposDePropriedades()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Clientes.Add(new Cliente
            {
                Nome = "Fulano de Tal",
                Telefone = "129998877665",
                Endereco = new Endereco { Logradouro = "Rua 1", Bairro = "Teste", Cidade = "São José dos Campos", Estado = "SP" }
            });

            db.SaveChanges();

            var clientes = db.Clientes.AsNoTracking().ToList();
            foreach (var cliente in clientes)
                Console.WriteLine(cliente);
        }

        static void TrabalhandoComPropriedadesDeSombra()
        {
            using var db = new ApplicationContext();
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();

            //var departamento = new Departamento() { Descricao = "Teste shadow" };
            //db.Departamentos.Add(departamento);
            //db.Entry(departamento).Property("UltimaAtualizacao").CurrentValue = DateTime.Now;
            //db.SaveChanges();

            var deparamentos = db.Departamentos.Where(_ => EF.Property<DateTime>(_, "UltimaAtualizacao") < DateTime.Now);
        }


        static void PropriedadesDeSombra()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        static void ConversorDeValorCustomizado()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Conversores.Add(new Conversor() { Status = Status.Analise });
            db.SaveChanges();

            var conversoresEmAnalise = db.Conversores.AsNoTracking().FirstOrDefault(_ => _.Status == Status.Analise);
            var conversoresDevolvidos = db.Conversores.AsNoTracking().FirstOrDefault(_ => _.Status == Status.Devolvido);
        }
        static void ConversorDeValor() => Esquema();

        static void Esquema()
        {
            using var db = new ApplicationContext();

            var script = db.Database.GenerateCreateScript();
            Console.WriteLine(script);
        }

        static void PropagarDados()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var script = db.Database.GenerateCreateScript();
            Console.WriteLine(script);
        }

        static void Collations()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
