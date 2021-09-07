namespace Curso.Dominando.EFCore.Domain
{
    public class Funcionario
    {
        public Funcionario()
        {

        }

        public Funcionario(string nome, string cPF, string rG, int departamentoId)
            : this(nome, cPF, rG)
        {
            DepartamentoId = departamentoId;
        }

        public Funcionario(string nome, string cPF, string rG)
        {
            Nome = nome;
            CPF = cPF;
            RG = rG;            
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public int DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }

        public override string ToString()
        {
            return $"[Nome={Nome}, CPF={CPF}, RG={RG}, DepartamentoId={DepartamentoId}]";
        }

    }
}
