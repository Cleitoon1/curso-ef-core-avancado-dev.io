using Curso.Dominando.EFCore.Modulo18.UOWRepository.Domain;
using Curso.Dominando.EFCore.Modulo18.UOWRepository.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Dominando.EFCore.Modulo18.UOWRepository.Data.Repositories
{
    public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamentoRepository
    {
        private readonly ApplicationContext _db;
        private readonly DbSet<Departamento> _dbSet;

        public DepartamentoRepository(ApplicationContext db) : base(db)
        {
            _db = db;
            _dbSet = _db.Set<Departamento>();
        }
    }
}
