using SalesProject.Domain.Interfaces;
using SalesProject.Infra.Context;

namespace SalesProject.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext) =>
            _dataContext = dataContext;

        public bool Commit() =>
            _dataContext.SaveChanges() > 0;

        public void Rollback() { }

        public void Dispose() =>
            _dataContext.Dispose();
    }
}