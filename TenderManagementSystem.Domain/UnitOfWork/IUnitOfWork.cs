namespace TenderManagementSystem.Domain.UnitOfWork
{
    using Repositories;

    public interface IUnitOfWork
    {
        ITenderRepository Tenders { get; }
        void StartTransaction();
        void Commit();
        void Rollback();
    }
}
