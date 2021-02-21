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
    
    public class GetAllQuery : IRequest<TenderVM>
    {
        public class GetAllQueryHandler : ApplicationBase, IRequestHandler<GetAllQuery, TenderVM>
        {
            public GetAllQueryHandler(IConfigConstants constant, IMapper mapper, IUnitOfWork unitOfWork)
                : base(constant, unitOfWork, mapper)
            {
            }

            public async Task<TenderVM> Handle(GetAllQuery request, CancellationToken cancellationToken)
            {
                var res = Mapper.Map(UnitOfWork.Tenders.GetAll().Result, new List<TenderDTO>());
                return await Task.FromResult(new TenderVM() { TenderList = res });
            }
        }
    }
}
