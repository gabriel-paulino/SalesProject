using SalesProject.Domain.Interfaces;
using SalesProject.Infra.Context;

namespace SalesProject.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext) =>
            _dataContext = dataContext;

        public void Commit() =>
            _dataContext.SaveChanges();
        
        public void Rollback() { }

        public void Dispose() =>
            _dataContext.Dispose();
    }
}