namespace TenderManagementSystem.Application.UnitTests.Tender.Queries
{
    using Application.Tender.Queries;
    using Application.Tender.VM;
    using AutoMapper;
    using Common.Interfaces;
    using Domain.UnitOfWork;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TenderCollection")]
    public class GetOneQueryTest
    {
        private readonly IConfigConstants _configConstants;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetOneQueryTest(TenderFixture fixture)
        {
            _mapper = fixture.Mapper;
            _configConstants = fixture.ConfigConstants;
            _unitOfWork = fixture.UnitOfWork;
        }

        [Fact]
        public async Task Handle_ReturnCorrectVM()
        {
            var query = new GetOneQuery
            {
                Id = 110,
            };

            var handler = new GetOneQuery.GetOneQueryHandler(_configConstants, _mapper, _unitOfWork);
            var result = await handler.Handle(query, CancellationToken.None);
            result.ShouldBeOfType<TenderVM>();
        }

        [Fact]
        public async Task Handle_ReturnCorrectTenderName_WhenProvided()
        {
            var query = new GetOneQuery
            {
                Id = 110,
            };

            var handler = new GetOneQuery.GetOneQueryHandler(_configConstants, _mapper, _unitOfWork);
            var result = await handler.Handle(query, CancellationToken.None);
            result.TenderList[0].Name.ShouldBe("Tender Name 110");
        }

        [Fact]
        public async Task Handle_ReturnCorrectContractNumber_WhenProvided()
        {
            var query = new GetOneQuery
            {
                Id = 110,
            };

            var handler = new GetOneQuery.GetOneQueryHandler(_configConstants, _mapper, _unitOfWork);
            var result = await handler.Handle(query, CancellationToken.None);
            result.TenderList[0].ContractNumber.ShouldBe("Contract Number 110");
        }
    }
}
