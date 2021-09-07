using Curso.Dominando.EFCore.Modulo18.UOWRepository.Data;
using Curso.Dominando.EFCore.Modulo18.UOWRepository.Domain;
using Curso.Dominando.EFCore.Modulo18.UOWRepository.Data.Repositories.Base;

namespace Curso.Dominando.EFCore.Modulo18.UOWRepository.Data.Repositories
{
    public class ColaboradorRepository : GenericRepository<Colaborador>, IColaboradorRepository
    {
        public ColaboradorRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
