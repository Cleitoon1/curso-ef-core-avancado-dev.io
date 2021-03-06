using System.Collections.Generic;

namespace Curso.Dominando.EFCore.Modulo14.Performance.Domain
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }
        public byte[] Image { get; set; }
        public virtual IList<Funcionario> Funcionarios { get; set; }

        public override string ToString()
            => $"[Id={Id}, Descricao={Descricao}, Ativo={Ativo}, Excluido={Excluido}, Funcionarios=[{(Funcionarios != null ? string.Join(", ", Funcionarios) : "")}]]";
    }
}
