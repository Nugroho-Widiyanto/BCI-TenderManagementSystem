namespace TenderManagementSystem.Persistence.Repositories
{
    using Dapper;
    using Dapper.Contrib.Extensions;
    using Domain.Entities;
    using Domain.Repositories;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class TenderRepository : Repository, ITenderRepository
    {
        public TenderRepository(IDbConnection dbConnection, IDbTransaction dbTransaction)
            : base(dbConnection, dbTransaction)
        {
        }

        public Task<int> Add(Tender tender)
        {
            return DbConnection.InsertAsync(tender, DbTransaction);
        }

        public Task<bool> Delete(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            var result = DbConnection.ExecuteAsync(sql: "Usp_DeleteTender", param: parameters, transaction: DbTransaction, commandType: CommandType.StoredProcedure).Result;
            return Task.FromResult(result == 1);
        }

        public Task<Tender> GetOne(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            var result = DbConnection.QueryAsync<Tender>(sql: "Usp_GetTender", param: parameters, transaction: DbTransaction, commandType: CommandType.StoredProcedure).Result.SingleOrDefault();
            return Task.FromResult(result);
        }

        public Task<IEnumerable<Tender>> GetAll()
        {
            return DbConnection.GetAllAsync<Tender>();
        }

        public Task<bool> Update(Tender tender)
        {
            return DbConnection.UpdateAsync(tender, DbTransaction);
        }

        public Task<bool> DeleteAll()
        {
            return DbConnection.DeleteAllAsync<Tender>();
        }
    }
}
