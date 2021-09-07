using Curso.Dominando.EFCore.Modulo11.Interceptacao.Data;
using Curso.Dominando.EFCore.Modulo11.Interceptacao.Domain;
using System;
using System.Linq;

namespace Curso.Dominando.EFCore.Modulo11.Interceptacao
{
    class Program
    {
        static void Main(string[] args)
        {
            //TesteInterceptacao();
            TesteInterceptacaoSaveChanges();
        }

        static void TesteInterceptacaoSaveChanges()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Funcoes.Add(new Funcao
                {
                    Descricao1 = "Teste"
                });

                db.SaveChanges();
            }
        }

        static void TesteInterceptacao()
        {
            using (var db = new ApplicationContext())
            {
                //var resultado = db.Funcoes.TagWith("Use NoLock").FirstOrDefault();
                var resultado = db.Funcoes.FirstOrDefault();
                Console.WriteLine($"Consulta: {resultado?.Descricao1}");
            }
        }
    }
}
