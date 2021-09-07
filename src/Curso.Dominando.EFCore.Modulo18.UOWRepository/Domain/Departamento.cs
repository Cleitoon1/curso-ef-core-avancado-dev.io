using System.Collections.Generic;

namespace Curso.Dominando.EFCore.Modulo18.UOWRepository.Domain
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public IList<Colaborador> Colaboradores { get; set; }
    }
}
