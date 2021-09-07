using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Curso.Dominando.EFCore.Modulo9.DataAnnotations.Domain
{
    public class Aeroporto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [InverseProperty("ArtoportoDePartida")]
        public ICollection<Voo> VoosDePartida { get; set; }
        [InverseProperty("ArtoportoDeChegada")]
        public ICollection<Voo> VoosDeChegada { get; set; }

        [NotMapped]
        public string PropriedadeTeste { get; set; }
    }
}
