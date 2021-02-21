namespace TenderManagementSystem.Application.UnitTests.Tender.Queries
{
    using Application.Tender.Queries;
    using Application.Tender.VM;
    using AutoMapper;
    using Common.Interfaces;
    using Domain.UnitOfWork;
    using Shouldly;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;


    [Collection("TenderCollection")]
    public class GetAllQueryTest
    {
        private readonly IMapper _mapper;
        private readonly IConfigConstants _configConstants;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllQueryTest(TenderFixture fixture)
        {
            _mapper = fixture.Mapper;
            _configConstants = fixture.ConfigConstants;
            _unitOfWork = fixture.UnitOfWork;
        }

        [Fact]
        public async Task Handle_ReturnCorrectVM()
        {
            var query = new GetAllQuery();
            var handler = new GetAllQuery.GetAllQueryHandler(_configConstants, _mapper, _unitOfWork);
            var result = await handler.Handle(query, CancellationToken.None);
            result.ShouldBeOfType<TenderVM>();
        }

        [Fact]
        public async Task Handle_ReturnTwoRecords_WhenRun()
        {
            var query = new GetAllQuery();
            var handler = new GetAllQuery.GetAllQueryHandler(_configConstants, _mapper, _unitOfWork);
            var result = await handler.Handle(query, CancellationToken.None);
            result.TenderList.Count().ShouldBe(2);
        }
    }
}
