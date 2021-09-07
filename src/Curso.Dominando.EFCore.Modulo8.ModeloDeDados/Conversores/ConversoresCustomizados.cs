using Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Conversores
{
    public class ConversoresCustomizados : ValueConverter<Status, string>
    {
        public ConversoresCustomizados() : base(
            _ => ConverterParaBancoDeDados(_), 
            _ => ConverterParaAplicacao(_),
            new ConverterMappingHints(1))
        {

        }

        static string ConverterParaBancoDeDados(Status status) => status.ToString()[0..1];

        static Status ConverterParaAplicacao(string status) => Enum.GetValues<Status>().FirstOrDefault(_ => _.ToString()[0..1] == status);
    }
}
