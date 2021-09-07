using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.Dominando.EFCore.Modulo8.ModeloDeDados.Domain
{
    public class Governador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Partido { get; set; }
        public int EstadoReference { get; set; }
        public Estado Estado { get; set; }

        public override string ToString()
            => $"[Id={Id}, Nome={Nome}, Idade={Idade}, Partido={Partido}, EstadoReference={EstadoReference}]";
    }
}
