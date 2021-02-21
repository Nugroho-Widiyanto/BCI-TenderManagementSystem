namespace TenderManagementSystem.Application.UnitTests.Tender.Commands
{
    using Application.Tender.Commands;
    using Common.Interfaces;
    using Shouldly;
    using System.Linq;
    using Xunit;

    [Collection("TenderCollection")]
    public class UpdateCommandValidatorTest
    {
        private readonly IConfigConstants _configConstants;

        public UpdateCommandValidatorTest(TenderFixture fixture)
        {
            _configConstants = fixture.ConfigConstants;
        }

        [Fact]
        public void Validate_ReturnTrue_WhenDataIsValid()
        {
            var command = new UpdateCommand
            {
                Id = 100,
                Name = "Tender Name",
                ContractNumber = "Contract Number",
            };

            var validator = new UpdateCommandValidator(_configConstants);
            var result = validator.Validate(command);
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_ReturnFalse_WhenDataIsInvalid()
        {
            var command = new UpdateCommand
            {
                Id = 0,
                Name = "Tender Name",
                ContractNumber = "Contract Number",
            };

            var validator = new UpdateCommandValidator(_configConstants);
            var result = validator.Validate(command);
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validate_ReturnCorrectErrorMessage_WhenDataIsInvalid()
        {
            var command = new UpdateCommand();
            var validator = new UpdateCommandValidator(_configConstants);
            var result = validator.Validate(command);
            result.Errors.FirstOrDefault(e => e.ErrorMessage.Equals(_configConstants.INVALID_TENDER_ID)).ShouldNotBeNull();
            result.Errors.FirstOrDefault(e => e.ErrorMessage.Equals(_configConstants.INVALID_TENDER_NAME)).ShouldNotBeNull();
            result.Errors.FirstOrDefault(e => e.ErrorMessage.Equals(_configConstants.INVALID_TENDER_CONTRACT_NUMBER)).ShouldNotBeNull();
        }
    }
}
