using System.Collections.Generic;

namespace Curso.Dominando.EFCore.Modulo19.DicasETruques.Domain
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public IList<Colaborador> Colaboradores { get; set; }
    }
}
