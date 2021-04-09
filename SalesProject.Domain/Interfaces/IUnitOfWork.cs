using System;

namespace SalesProject.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
        void Rollback();
    }
}