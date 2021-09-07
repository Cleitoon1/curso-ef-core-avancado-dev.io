using Microsoft.EntityFrameworkCore;
using System;

namespace Curso.Dominando.EFCore.Modulo9.DataAnnotations.Domain
{
    [Keyless]
    public class RelatorioFinanceiro
    {
        public string Descricao { get; set; }
        public decimal Total { get; set; }
        public DateTime Data { get; set; }
    }
}
