namespace TenderManagementSystem.Application.UnitTests.Tender.Queries
{
    using Application.Tender.Queries;
    using Common.Interfaces;
    using Shouldly;
    using System.Linq;
    using Xunit;

    [Collection("TenderCollection")]
    public class GetOneQueryValidatorTest
    {
        private readonly IConfigConstants _configConstants;

        public GetOneQueryValidatorTest(TenderFixture fixture)
        {
            _configConstants = fixture.ConfigConstants;
        }

        [Fact]
        public void Validate_ReturnTrue_WhenDataIsValid()
        {
            var query = new GetOneQuery
            {
                Id = 100,
            };

            var validator = new GetOneQueryValidator(_configConstants);
            var result = validator.Validate(query);
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_ReturnFalse_WhenDataIsInvalid()
        {
            var query = new GetOneQuery
            {
                Id = 0,
            };

            var validator = new GetOneQueryValidator(_configConstants);
            var result = validator.Validate(query);
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validate_ReturnErrorMessage_WhenDataIsInvalid()
        {
            var query = new GetOneQuery
            {
                Id = 0,
            };

            var validator = new GetOneQueryValidator(_configConstants);
            var result = validator.Validate(query);
            result.Errors.FirstOrDefault(x => x.ErrorMessage == _configConstants.INVALID_TENDER_ID)?.ErrorMessage.ShouldBe(_configConstants.INVALID_TENDER_ID);
            result.IsValid.ShouldBeFalse();
        }
    }
}
