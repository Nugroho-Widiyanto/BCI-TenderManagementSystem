using TenderManagementSystem.Application.Common.Exceptions;

namespace TenderManagementSystem.Application.UnitTests.Tender.Commands
{
    using Application.Tender.Commands;
    using AutoMapper;
    using Common.Interfaces;
    using Domain.UnitOfWork;
    using Shouldly;
    using Shouldly.ShouldlyExtensionMethods;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TenderCollection")]
    public class UpdateCommandTest
    {
        private readonly IMapper _mapper;
        private readonly IConfigConstants _configConstants;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCommandTest(TenderFixture fixture)
        {
            _mapper = fixture.Mapper;
            _configConstants = fixture.ConfigConstants;
            _unitOfWork = fixture.UnitOfWork;
        }

        [Fact]
        public async Task Handle_ReturnTrue_WhenDataIsValid()
        {
            var command = new UpdateCommand
            {
                Id = 100,
                Name = "Tender Name",
                ContractNumber = "Contract Number",
            };

            var handler = new UpdateCommand.UpdateCommandHandler(_configConstants, _mapper, _unitOfWork);
            var result = await handler.Handle(command, CancellationToken.None);
            result.ShouldBe(true);
        }

        [Fact]
        public void Handle_ReturnFalse_WhenDataIsInvalid()
        {
            var command = new UpdateCommand
            {
                Id = 0,
            };

            var handler = new UpdateCommand.UpdateCommandHandler(_configConstants, _mapper, _unitOfWork);
            Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None)).Result.Message.ShouldBe("The Tender ID 0 is not found");
        }
    }
}
