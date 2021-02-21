namespace TenderManagementSystem.Application.Tender.Commands
{
    using Common.Interfaces;
    using FluentValidation;

    public class AddCommandValidator : AbstractValidator<AddCommand>
    {
        public AddCommandValidator(IConfigConstants constant)
        {
            this.RuleFor(v => v.Name).NotEmpty().WithMessage(constant.INVALID_TENDER_NAME);
            this.RuleFor(v => v.ContractNumber).NotEmpty().WithMessage(constant.INVALID_TENDER_CONTRACT_NUMBER);
        }
    }
}
