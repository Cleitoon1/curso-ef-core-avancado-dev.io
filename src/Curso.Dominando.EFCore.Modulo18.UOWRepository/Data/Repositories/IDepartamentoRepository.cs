using Curso.Dominando.EFCore.Modulo18.UOWRepository.Domain;
using Curso.Dominando.EFCore.Modulo18.UOWRepository.Data.Repositories.Base;
using System.Threading.Tasks;

namespace Curso.Dominando.EFCore.Modulo18.UOWRepository.Data.Repositories
{
    public interface IDepartamentoRepository : IGenericRepository<Departamento>
    {
    }
}
