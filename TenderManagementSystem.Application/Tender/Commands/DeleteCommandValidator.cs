namespace TenderManagementSystem.Application.Tender.Commands
{
    using Common.Interfaces;
    using FluentValidation;

    public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DeleteCommandValidator(IConfigConstants constant)
        {
            this.RuleFor(v => v.Id).GreaterThan(0).WithMessage(constant.INVALID_TENDER_ID);
        }
    }
}
