namespace TenderManagementSystem.Persistence.UnitOfWork
{
    using Domain.UnitOfWork;
    using Repositories;
    using System.Data;
    using TenderManagementSystem.Domain.Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;

        public ITenderRepository Tenders => new TenderRepository(this._dbConnection, this._dbTransaction);

        public UnitOfWork(IDbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
            this.ManageConnection();
        }

        public void StartTransaction()
        {
            this._dbTransaction ??= this._dbConnection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                this._dbTransaction.Commit();
            }
            catch
            {
                this._dbTransaction.Rollback();
            }
        }

        public void Rollback()
        {
            this._dbTransaction.Rollback();
        }

        private void ManageConnection()
        {
            if (this._dbConnection.State == ConnectionState.Closed)
            {
                this._dbConnection.Open();
            }
        }
    }
}
