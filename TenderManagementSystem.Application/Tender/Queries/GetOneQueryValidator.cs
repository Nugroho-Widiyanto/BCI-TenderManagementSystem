namespace TenderManagementSystem.Application.Tender.Queries
{
    using Common.Interfaces;
    using FluentValidation;

    public class GetOneQueryValidator : AbstractValidator<GetOneQuery>
    {
        public GetOneQueryValidator(IConfigConstants constant)
        {
            this.RuleFor(v => v.Id).GreaterThan(0).WithMessage(constant.INVALID_TENDER_ID);
        }
    }
}
