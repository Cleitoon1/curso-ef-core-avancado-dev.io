using Curso.Dominando.EFCore.Modulo13.UDFs.Data;
using Curso.Dominando.EFCore.Modulo13.UDFs.Domain;
using Curso.Dominando.EFCore.Modulo13.UDFs.Funcoes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Curso.Dominando.EFCore.Modulo13.UDFs
{
    class Program
    {
        static void Main(string[] args)
        {
            //FuncaoLeft();
            //FuncaoDefinidaPeloUsuario();
            DateDiff();
        }

        static void DateDiff()
        {
            CadastrarLivro();

            using var db = new ApplicationContext();

            var resultado = db.Livros.Select(_ => MinhasFuncoes.DateDiff("DAY", _.CadastradoEm, DateTime.Now));
            foreach (var item in resultado)
                Console.WriteLine(item);
        }

        static void FuncaoDefinidaPeloUsuario()
        {
            CadastrarLivro();

            using var db = new ApplicationContext();

            db.Database.ExecuteSqlRaw(@"
                CREATE FUNCTION ConverterParaLetrasMaiusculas(@dados VARCHAR(100))
                RETURNS VARCHAR(100)
                BEGIN
                    RETURN UPPER(@dados)
                END");

            var resultado = db.Livros.Select(_ => MinhasFuncoes.LetrasMaiusculas(_.Titulo));
            foreach (var item in resultado)
                Console.WriteLine(item);
        }

        static void FuncaoLeft()
        {
            CadastrarLivro();
            using var db = new ApplicationContext();
            var resultado = db.Livros.Select(_ => MinhasFuncoes.Left(_.Titulo, 10));
            foreach (var item in resultado)
                Console.WriteLine(item);
        }

        static void CadastrarLivro()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Introdução ao Entity Framework Core 5",
                        Autor = "Cleiton",
                        CadastradoEm = DateTime.Now.AddDays(-1)
                    });

                db.SaveChanges();
            }
        }
    }
}
