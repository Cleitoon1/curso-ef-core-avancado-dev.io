namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain
{
    public class Funcionario
    {
        public Funcionario()
        {

        }

        public Funcionario(string nome, string cPF, string rg, int departamentoId)
            : this(nome, cPF, rg)
        {
            //DepartamentoId = departamentoId;
        }

        public Funcionario(string nome, string cPF, string rg)
        {
            Nome = nome;
            CPF = cPF;
            RG = rg;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public bool Excluido { get; set; }
        //public int DepartamentoId { get; set; }

        public virtual Departamento Departamento { get; set; }

        public override string ToString() => $"[Nome={Nome}, CPF={CPF}, RG={RG}, Excluido={Excluido}, DepartamentoId=]";
    }
}
