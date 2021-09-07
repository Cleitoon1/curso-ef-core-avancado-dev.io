namespace Curso.Dominando.EFCore.Modulo9.DataAnnotations.Domain
{

    public class Voo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Aeroporto ArtoportoDePartida { get; set; }
        public Aeroporto ArtoportoDeChegada { get; set; }
    }
}
