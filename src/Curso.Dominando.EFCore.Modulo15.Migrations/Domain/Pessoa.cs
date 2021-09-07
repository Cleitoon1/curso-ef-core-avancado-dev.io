namespace Curso.Dominando.EFCore.Modulo15.Migrations.Domain
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }

        public override string ToString() => $"[Id={Id}, Nome={Nome}]";
    }
}
