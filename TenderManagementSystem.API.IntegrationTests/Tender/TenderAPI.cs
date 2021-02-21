namespace TenderManagementSystem.API.IntegrationTests.Tender
{
    using Shouldly;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using TenderManagementSystem.Application.Tender.Commands;
    using Xunit;

    public class TenderAPI : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private const string EndPoint = "api/Tender";

        public TenderAPI(CustomWebApplicationFactory<Startup> factory)
        {
            this._factory = factory;
        }

        [Fact]
        public async Task GivenValidGetAllQuery_ReturnSuccessObject()
        {
            await AddNewTender();
            var client = this._factory.GetAnonymousClient();
            var response = await client.GetAsync($"{EndPoint}/GetAll");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenValidGetOneQuery_ReturnSuccessObject()
        {
            var res = await AddNewTender();
            var client = this._factory.GetAnonymousClient();
            var response = await client.GetAsync($"{EndPoint}/GetOne?Id={res}");
            var result = response.Content.ReadAsStringAsync().Result;
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenValidAddCommand_ReturnSuccessObject()
        {
            var res = await AddNewTender();
            res.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GivenValidUpdateCommand_ReturnSuccessObject()
        {
            var res = await AddNewTender();
            var client = this._factory.GetAnonymousClient();

            var command = new UpdateCommand
            {
                Id = res,
                Name = "Tender Name Updated",
                ContractNumber = "Contract Number Updated",
            };

            var content = IntegrationTestHelper.GetRequestContent(command);
            var response = await client.PutAsync($"{EndPoint}/Put", content);
            response.EnsureSuccessStatusCode();

        }

        [Fact]
        public async Task GivenValidDeleteCommand_ReturnSuccessObject()
        {
            var res = await AddNewTender();
            var client = this._factory.GetAnonymousClient();
            var response = await client.DeleteAsync($"{EndPoint}/Delete?Id={res}");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenValidGetOneQuery_SendNullId_ReturnErrorCode()
        {
            var client = this._factory.GetAnonymousClient();
            var response = await client.GetAsync($"{EndPoint}/GetOne");
            var result = response.Content.ReadAsStringAsync().Result;
            result.ShouldContain("Tender ID is required!");
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        private async Task<int> AddNewTender()
        {
            var client = this._factory.GetAnonymousClient();
            var command = new AddCommand
            {
                Name = "Tender Name",
                ContractNumber = "Contract Number",
                ReleaseDate = new DateTime(2021, 01, 01).ToString("yyyy-MM-dd"),
                ClosingDate = new DateTime(2021, 02, 01).ToString("yyyy-MM-dd"),
                Description = "Tender Description",
            };

            var content = IntegrationTestHelper.GetRequestContent(command);
            var response = await client.PostAsync($"{EndPoint}/Post", content);
            return Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
        }
    }
}
