namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain
{
    public class Cidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
        public override string ToString() => $"[Id={Id}, Nome={Nome}, EstadoId={EstadoId}]";
    }
}
