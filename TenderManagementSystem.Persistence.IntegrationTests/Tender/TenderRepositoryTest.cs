namespace TenderManagementSystem.Persistence.IntegrationTests.Tender
{
    using Shouldly;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using TenderManagementSystem.Domain.Repositories;
    using Xunit;

    [Collection("TenderCollection")]
    public class TenderRepositoryTest : IDisposable
    {
        private readonly ITenderRepository _repository;

        public TenderRepositoryTest(TenderFixture fixture)
        {
            _repository = fixture.TenderRepository;
        }

        [Fact]
        public async Task TestAdd_GivenCorrectParam_ReturnId()
        {
            var res = await AddNewTender();
            res.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task TestGetAll_GivenCorrectParam_ReturnList()
        {
            await AddNewTender();
            var res = await _repository.GetAll();
            res.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task TestGetOne_GivenCorrectParam_ReturnTender()
        {
            var id = await AddNewTender();
            var res = await _repository.GetOne(id);
            res.ShouldBeOfType<Domain.Entities.Tender>();
        }

        [Fact]
        public async Task TestUpdate_GivenCorrectParam_ReturnTrue()
        {
            var id = AddNewTender().Result;

            var tender = new Domain.Entities.Tender
            {
                Id = id,
                Name = "Tender Name Updated",
                ContractNumber = "Contract Number Updated",
                ReleaseDate = new DateTime(2021, 01, 01),
                ClosingDate = new DateTime(2021, 02, 01),
                Description = "Tender Description Updated",
            };

            var res = await _repository.Update(tender);
            res.ShouldBeTrue();
        }

        [Fact]
        public async Task TestDelete_GivenCorrectParam_ReturnTrue()
        {
            var id = AddNewTender().Result;
            var res = await _repository.Delete(id);
            res.ShouldBeTrue();
        }

        private async Task<int> AddNewTender()
        {
            var tender = new Domain.Entities.Tender
            {
                Name = "Tender Name",
                ContractNumber = "Contract Number",
                ReleaseDate = new DateTime(2021, 01, 01),
                ClosingDate = new DateTime(2021, 02, 01),
                Description = "Tender Description",
            };

            return await _repository.Add(tender);
        }

        public void Dispose()
        {
            _repository.DeleteAll();
        }
    }
}
