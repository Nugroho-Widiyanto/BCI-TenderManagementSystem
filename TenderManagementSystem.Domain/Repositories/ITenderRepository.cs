namespace TenderManagementSystem.Domain.Repositories
{
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITenderRepository
    {
        Task<int> Add(Tender tender);
        Task<bool> Update(Tender tender);
        Task<bool> Delete(int id);
        Task<Tender> GetOne(int id);
        Task<IEnumerable<Tender>> GetAll();
        Task<bool> DeleteAll();
    }
}
