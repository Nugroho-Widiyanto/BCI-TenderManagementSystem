using System;

namespace TenderManagementSystem.Application.UnitTests.Tender.Commands
{
    using Application.Tender.Commands;
    using AutoMapper;
    using Common.Interfaces;
    using Domain.UnitOfWork;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TenderCollection")]
    public class AddCommandTest
    {
        private readonly IMapper _mapper;
        private readonly IConfigConstants _configConstants;
        private readonly IUnitOfWork _unitOfWork;

        public AddCommandTest(TenderFixture fixture)
        {
            _mapper = fixture.Mapper;
            _configConstants = fixture.ConfigConstants;
            _unitOfWork = fixture.UnitOfWork;
        }

        [Fact]
        public async Task Handle_ReturnCorrectVM()
        {
            var command = new AddCommand
            {
                Name = "Tender Name",
                ContractNumber = "Contract Number",
                ReleaseDate = DateTime.Today.ToString("yyyy-MM-dd"),
                ClosingDate = DateTime.Today.AddMonths(1).ToString("yyyy-MM-dd"),
                Description = "Tender Description",
            };

            var handler = new AddCommand.AddCommandHandler(_configConstants, _mapper, _unitOfWork);
            var result = await handler.Handle(command, CancellationToken.None);
            result.ShouldBeOfType<int>();
        }

        [Fact]
        public async Task Handle_ReturnTrue_WhenSendCorrectPayload()
        {
            var command = new AddCommand
            {
                Name = "Tender Name",
                ContractNumber = "Contract Number",
                ReleaseDate = DateTime.Today.ToString("yyyy-MM-dd"),
                ClosingDate = DateTime.Today.AddMonths(1).ToString("yyyy-MM-dd"),
                Description = "Tender Description",
            };

            var handler = new AddCommand.AddCommandHandler(_configConstants, _mapper, _unitOfWork);
            var result = await handler.Handle(command, CancellationToken.None);
            result.ShouldBe(100);
        }
    }
}
