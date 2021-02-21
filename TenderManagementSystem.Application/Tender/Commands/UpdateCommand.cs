namespace TenderManagementSystem.Application.Tender.Commands
{
    using AutoMapper;
    using Common.BaseClass;
    using Common.Exceptions;
    using Common.Interfaces;
    using Domain.UnitOfWork;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContractNumber { get; set; }
        public string ReleaseDate { get; set; }
        public string ClosingDate { get; set; }
        public string Description { get; set; }

        public class UpdateCommandHandler : ApplicationBase, IRequestHandler<UpdateCommand, bool>
        {
            public UpdateCommandHandler(IConfigConstants constant, IMapper mapper, IUnitOfWork unitOfWork)
                : base(constant, unitOfWork, mapper)
            {
            }

            public async Task<bool> Handle(UpdateCommand request, CancellationToken cancellationToken)
            {
                var tender = await this.UnitOfWork.Tenders.GetOne(request.Id);
                if (tender == null) throw new NotFoundException($"The Tender ID {request.Id} is not found");

                tender.Name = request.Name;
                tender.ContractNumber = request.ContractNumber;
                tender.ReleaseDate =
                    string.IsNullOrWhiteSpace(request.ReleaseDate)
                        ? (DateTime?) null
                        : DateTime.TryParse(request.ReleaseDate, out var releaseDate)
                            ? releaseDate
                            : (DateTime?) null;
                tender.ClosingDate =
                    string.IsNullOrWhiteSpace(request.ClosingDate)
                        ? (DateTime?) null
                        : DateTime.TryParse(request.ClosingDate, out var closingDate)
                            ? closingDate
                            : (DateTime?) null;
                tender.Description = request.Description;

                this.UnitOfWork.StartTransaction();
                var res = UnitOfWork.Tenders.Update(tender).Result;
                this.UnitOfWork.Commit();
                return await Task.Run(() => res, cancellationToken);
            }
        }
    }
}
