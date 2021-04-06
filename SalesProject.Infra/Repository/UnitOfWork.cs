using SalesProject.Context;
using SalesProject.Infra.Interface;

namespace SalesProject.Infra.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesProjectDataContext _salesProjectDataContext;

        public UnitOfWork(SalesProjectDataContext salesProjectDataContext)
        {
            _salesProjectDataContext = salesProjectDataContext;
        }

        public void Commit()
        {
            _salesProjectDataContext.SaveChanges();
        }

        public void Rollback() { }

    }
}