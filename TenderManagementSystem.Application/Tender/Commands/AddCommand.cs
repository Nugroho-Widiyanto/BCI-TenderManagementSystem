namespace TenderManagementSystem.Application.Tender.Commands
{
    using AutoMapper;
    using Common.BaseClass;
    using Common.Interfaces;
    using Domain.Entities;
    using Domain.UnitOfWork;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string ContractNumber { get; set; }
        public string ReleaseDate { get; set; }
        public string ClosingDate { get; set; }
        public string Description { get; set; }

        public class AddCommandHandler : ApplicationBase, IRequestHandler<AddCommand, int>
        {
            public AddCommandHandler(IConfigConstants constant, IMapper mapper, IUnitOfWork unitOfWork)
                : base(constant, unitOfWork, mapper)
            {
            }

            public async Task<int> Handle(AddCommand request, CancellationToken cancellationToken)
            {
                var tender = new Tender
                {
                    Name = request.Name,
                    ContractNumber = request.ContractNumber,
                    ReleaseDate = 
                        string.IsNullOrWhiteSpace(request.ReleaseDate) 
                            ? (DateTime?) null 
                            : DateTime.TryParse(request.ReleaseDate, out var releaseDate) 
                                ? releaseDate 
                                : (DateTime?) null,
                    ClosingDate = 
                        string.IsNullOrWhiteSpace(request.ClosingDate) 
                            ? (DateTime?) null 
                            : DateTime.TryParse(request.ClosingDate, out var closingDate) 
                                ? closingDate 
                                : (DateTime?) null,
                    Description = request.Description
                };

                this.UnitOfWork.StartTransaction();
                var res = UnitOfWork.Tenders.Add(tender).Result;
                this.UnitOfWork.Commit();
                return await Task.Run(() => res, cancellationToken);
            }
        }
    }
}
