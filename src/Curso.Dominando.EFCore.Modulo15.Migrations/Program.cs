using Curso.Dominando.EFCore.Modulo15.Migrations.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Curso.Dominando.EFCore.Modulo15.Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new ApplicationContext();
            //db.Database.Migrate();
            foreach (var item in db.Database.GetPendingMigrations())
            {
                Console.WriteLine(item);
            }
        }
    }
}
