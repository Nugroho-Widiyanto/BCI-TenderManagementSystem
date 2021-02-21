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
    public class DeleteCommandTest
    {
        private readonly IMapper _mapper;
        private readonly IConfigConstants _configConstants;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCommandTest(TenderFixture fixture)
        {
            _mapper = fixture.Mapper;
            _configConstants = fixture.ConfigConstants;
            _unitOfWork = fixture.UnitOfWork;
        }

        [Fact]
        public async Task Handle_ReturnTrue_WhenDataIsValid()
        {
            var command = new DeleteCommand
            {
                Id = 100,
            };

            var handler = new DeleteCommand.DeleteCommandHandler(_configConstants, _mapper, _unitOfWork);
            var result = await handler.Handle(command, CancellationToken.None);
            result.ShouldBe(true);

        }

        [Fact]
        public async Task Handle_ReturnFalse_WhenDataIsInvalid()
        {
            var command = new DeleteCommand
            {
                Id = 0,
            };

            var handler = new DeleteCommand.DeleteCommandHandler(_configConstants, _mapper, _unitOfWork);
            var result = await handler.Handle(command, CancellationToken.None);
            result.ShouldBe(false);
        }
    }
}
