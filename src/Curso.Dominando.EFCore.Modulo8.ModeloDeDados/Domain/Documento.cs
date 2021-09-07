using System;

namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain
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

        public string CPF => _cpf;
        public string GetCpf() => _cpf;
    }
}
