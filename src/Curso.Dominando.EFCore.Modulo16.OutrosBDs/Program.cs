using Curso.Dominando.EFCore.Modulo16.OutrosBDs.Data;
using System;
using System.Linq;

namespace Curso.Dominando.EFCore.Modulo16.OutrosBDs
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();

            db.Pessoas.Add(new Domain.Pessoa
            {
                Id = 1,
                Nome = "Cleiton",
                Telefone = "1239221609"
            });
            db.SaveChanges();

            db.Pessoas.ToList().ForEach(item => Console.WriteLine($"{item.Nome}, {item.Telefone}"));
        }
    }
}
