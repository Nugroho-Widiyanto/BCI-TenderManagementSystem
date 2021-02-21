namespace TenderManagementSystem.Application.Tender.Commands
{
    using Common.Interfaces;
    using FluentValidation;

    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator(IConfigConstants constant)
        {
            this.RuleFor(v => v.Id).GreaterThan(0).WithMessage(constant.INVALID_TENDER_ID);
            this.RuleFor(v => v.Name).NotEmpty().WithMessage(constant.INVALID_TENDER_NAME);
            this.RuleFor(v => v.ContractNumber).NotEmpty().WithMessage(constant.INVALID_TENDER_CONTRACT_NUMBER);
        }
    }
}
