namespace TenderManagementSystem.Application.UnitTests.Tender.Commands
{
    using Application.Tender.Commands;
    using Common.Interfaces;
    using Shouldly;
    using Xunit;

    [Collection("TenderCollection")]
    public class DeleteCommandValidatorTest
    {
        private readonly IConfigConstants _configConstants;

        public DeleteCommandValidatorTest(TenderFixture fixture)
        {
            _configConstants = fixture.ConfigConstants;
        }

        [Fact]
        public void Validate_ReturnTrue_WhenDataIsValid()
        {
            var command = new DeleteCommand
            {
                Id = 100,
            };

            var validator = new DeleteCommandValidator(_configConstants);
            var result = validator.Validate(command);
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_ReturnFalse_WhenDataIsInvalid()
        {
            var command = new DeleteCommand
            {
                Id = 0,
            };

            var validator = new DeleteCommandValidator(_configConstants);
            var result = validator.Validate(command);
            result.IsValid.ShouldBeFalse();
        }
    }
}
