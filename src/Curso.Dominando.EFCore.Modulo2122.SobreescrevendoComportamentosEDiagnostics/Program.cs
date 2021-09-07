using Curso.Dominando.EFCore.Modulo2122.ComportamentosEDiagnostics.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace Curso.Dominando.EFCore.Modulo2122.ComportamentosEDiagnostics
{
    class Program
    {
        static void Main(string[] args)
        {
            DiagnosticListener.AllListeners.Subscribe(new MyInterceptorListener());

            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            //var sql = db.Departamentos.Where(_ => _.Id > 0).ToQueryString();
            _ = db.Departamentos.Where(_ => _.Id > 0).ToArray();
            //Console.WriteLine(sql);
        }
    }
}
