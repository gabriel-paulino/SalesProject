using SalesProject.Infra.Context;
using SalesProject.Domain.Interface;

namespace SalesProject.Infra.Repository
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly SalesProjectDataContext _salesProjectDataContext;

        public UnitOfWorkRepository(SalesProjectDataContext salesProjectDataContext)
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