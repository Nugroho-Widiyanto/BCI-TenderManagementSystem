namespace TenderManagementSystem.Application.Tender.Queries
{
    using AutoMapper;
    using Common.BaseClass;
    using Common.Interfaces;
    using Domain.UnitOfWork;
    using DTO;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using VM;

    public class GetOneQuery : IRequest<TenderVM>
    {
        public int Id { get; set; }

        public class GetOneQueryHandler : ApplicationBase, IRequestHandler<GetOneQuery, TenderVM>
        {
            public GetOneQueryHandler(IConfigConstants constant, IMapper mapper, IUnitOfWork unitOfWork)
                : base(constant, unitOfWork, mapper)
            {
            }

            public async Task<TenderVM> Handle(GetOneQuery request, CancellationToken cancellationToken)
            {
                var res = Mapper.Map(UnitOfWork.Tenders.GetOne(request.Id).Result, new TenderDTO());
                return await Task.FromResult(new TenderVM { TenderList = new List<TenderDTO> { res } });
            }
        }
    }
}