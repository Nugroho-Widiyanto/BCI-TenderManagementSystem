namespace TenderManagementSystem.Persistence.IntegrationTests.Tender
{
    using System.Data;
    using System.Data.SqlClient;
    using TenderManagementSystem.Domain.Repositories;
    using TenderManagementSystem.Persistence.Repositories;
    using Xunit;

    public class TenderFixture
    {
        public ITenderRepository TenderRepository { get; }

        public TenderFixture()
        {
            IDbConnection dbConnection = new SqlConnection("Data Source=localhost; Persist Security Info=True; Integrated Security=SSPI; Initial Catalog=NWY-TEST");
            TenderRepository = new TenderRepository(dbConnection, null);
        }

        [CollectionDefinition("TenderCollection")]
        public class QueryCollection : ICollectionFixture<TenderFixture>
        {
        }
    }
}
