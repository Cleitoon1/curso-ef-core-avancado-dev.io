using Microsoft.EntityFrameworkCore;
using System;

namespace Curso.Dominando.EFCore.Modulo9.DataAnnotations.Domain
{
    public class Documento
    {
        private string _cpf;

        public int Id { get; set; }

        public void SetCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new Exception("CPF Inválido");
            _cpf = cpf;
        }

        [BackingField(nameof(_cpf))]
        public string CPF => _cpf;

        public string GetCpf() => _cpf;
    }
}
