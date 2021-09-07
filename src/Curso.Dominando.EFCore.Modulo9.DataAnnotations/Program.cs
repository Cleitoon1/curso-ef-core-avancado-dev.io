using Curso.Dominando.EFCore.Modulo9.DataAnnotations.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Curso.Dominando.EFCore.Modulo9.DataAnnotations
{
    class Program
    {
        static void Main(string[] args)
        {
            Atributos();
        }

        static void Atributos()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                var script = db.Database.GenerateCreateScript();
                Console.WriteLine(script);
            }
            
        }
    }
}
