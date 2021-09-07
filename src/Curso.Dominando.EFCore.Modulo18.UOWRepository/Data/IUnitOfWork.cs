using Curso.Dominando.EFCore.Modulo18.UOWRepository.Data.Repositories;
using System;

namespace Curso.Dominando.EFCore.Modulo18.UOWRepository.Data
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
        IDepartamentoRepository DepartamentoRepository { get; }
    }
}
