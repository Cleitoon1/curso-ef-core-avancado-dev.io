using System.Collections.Generic;

namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Governador Governador { get; set; }
        public ICollection<Cidade> Cidades { get; } = new List<Cidade>();

        public override string ToString() => $"[Id={Id}, Nome={Nome}, Governador={Governador}, Cidades=[{string.Join(',', Cidades)}]]";
    }

}
