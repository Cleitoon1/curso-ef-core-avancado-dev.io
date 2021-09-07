using Curso.Dominando.EFCore.Modulo18.UOWRepository.Data;
using Curso.Dominando.EFCore.Modulo18.UOWRepository.Data.Repositories;
using System;

namespace Curso.Dominando.EFCore.Modulo18.UOWRepository.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        private IDepartamentoRepository _departamentoRepository;
        public IDepartamentoRepository DepartamentoRepository
        {
            get => _departamentoRepository ?? (_departamentoRepository = new DepartamentoRepository(_context));
        }

        public bool Commit() => _context.SaveChanges() > 0;
        public void Dispose() => _context.Dispose();
        
    }
}
