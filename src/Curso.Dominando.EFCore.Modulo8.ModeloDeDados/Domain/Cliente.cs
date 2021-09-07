namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }

        public override string ToString()
        {
            return $"[Id={Id}, Nome={Nome}, Telefone={Telefone}, Endereco={Endereco}]";
        }
    }

    public class Endereco
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public override string ToString() => $"[Logradouro={Logradouro}, Bairro={Bairro}, Cidade={Cidade}, Estado={Estado}]";
        
    }
}
