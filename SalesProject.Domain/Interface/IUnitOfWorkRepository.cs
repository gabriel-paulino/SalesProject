namespace SalesProject.Domain.Interface
{
    public interface IUnitOfWorkRepository
    {
        void Commit();
        void Rollback();
    }
}