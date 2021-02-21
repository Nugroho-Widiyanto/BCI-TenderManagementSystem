namespace TenderManagementSystem.Application.UnitTests.Tender.Commands
{
    using Application.Tender.Commands;
    using Common.Interfaces;
    using Shouldly;
    using System;
    using System.Linq;
    using Xunit;

    [Collection("TenderCollection")]
    public class AddCommandValidatorTest
    {
        private readonly IConfigConstants _configConstants;

        public AddCommandValidatorTest(TenderFixture fixture)
        {
            _configConstants = fixture.ConfigConstants;
        }

        [Fact]
        public void Validate_ReturnTrue_WhenDataIsValid()
        {
            var command = new AddCommand
            {
                Name = "Tender Name",
                ContractNumber = "Contract Number",
                ReleaseDate = DateTime.Today.ToString("yyyy-MM-dd"),
                ClosingDate = DateTime.Today.AddMonths(1).ToString("yyyy-MM-dd"),
                Description = "Tender Description",
            };

            var validator = new AddCommandValidator(_configConstants);
            var result = validator.Validate(command);
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_ReturnFalse_WhenTenderNameIsEmpty()
        {
            var command = new AddCommand
            {
                Name = string.Empty,
                ContractNumber = "Contract Number",
            };

            var validator = new AddCommandValidator(_configConstants);
            var result = validator.Validate(command);
            result.Errors.FirstOrDefault(x => x.ErrorMessage == _configConstants.INVALID_TENDER_NAME)?.ErrorMessage.ShouldBe(_configConstants.INVALID_TENDER_NAME);
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validate_ReturnFalse_WhenContractNumberIsEmpty()
        {
            var command = new AddCommand
            {
                Name = "Tender Number",
                ContractNumber = string.Empty,
            };

            var validator = new AddCommandValidator(_configConstants);
            var result = validator.Validate(command);
            result.Errors.FirstOrDefault(x => x.ErrorMessage == _configConstants.INVALID_TENDER_CONTRACT_NUMBER)?.ErrorMessage.ShouldBe(_configConstants.INVALID_TENDER_CONTRACT_NUMBER);
            result.IsValid.ShouldBeFalse();
        }

    }
}
