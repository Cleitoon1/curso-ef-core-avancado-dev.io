using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Curso.Dominando.EFCore.Modulo13.UDFs.Funcoes
{
    public static class MinhasFuncoes
    {
        [DbFunction(name: "LEFT", IsBuiltIn = true)]
        public static string Left(string dados, int quantitdade)
        {
            throw new NotImplementedException();
        }

        [DbFunction(name: "ConverterParaLetrasMaiusculas", Schema = "dbo")]
        public static string LetrasMaiusculas(string dados)
        {
            throw new NotImplementedException();
        }

        public static int DateDiff(string identificador, DateTime inicial, DateTime final)
        {
            throw new NotImplementedException();
        }

        public static void RegistrarFuncoes(ModelBuilder modelBuilder)
        {
            var funcoes = typeof(MinhasFuncoes).GetMethods().Where(_ => Attribute.IsDefined(_, typeof(DbFunctionAttribute)));
            foreach (var funcao in funcoes)
                modelBuilder.HasDbFunction(funcao);
        }

    }
}
