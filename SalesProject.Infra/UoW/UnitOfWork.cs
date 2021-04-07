using SalesProject.Infra.Context;
using SalesProject.Domain.Interfaces;

namespace SalesProject.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesProjectDataContext _salesProjectDataContext;

        public UnitOfWork(SalesProjectDataContext salesProjectDataContext) =>
            _salesProjectDataContext = salesProjectDataContext;

        public void Commit()
        {
            _salesProjectDataContext.SaveChanges();
        }

        public void Rollback() { }

        public void Dispose() =>
            _salesProjectDataContext.Dispose();
    }
}