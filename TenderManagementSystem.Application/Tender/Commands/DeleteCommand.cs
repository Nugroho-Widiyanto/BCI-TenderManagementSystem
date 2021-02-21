using System;

namespace TenderManagementSystem.Application.Tender.Commands
{
    using AutoMapper;
    using Common.BaseClass;
    using Common.Interfaces;
    using Domain.UnitOfWork;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public class DeleteCommandHandler : ApplicationBase, IRequestHandler<DeleteCommand, bool>
        {
            public DeleteCommandHandler(IConfigConstants constant, IMapper mapper, IUnitOfWork unitOfWork)
                : base(constant, unitOfWork, mapper)
            {
            }

            public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
            {
                var res = false;

                try
                {
                    this.UnitOfWork.StartTransaction();
                    res = UnitOfWork.Tenders.Delete(request.Id).Result;
                    this.UnitOfWork.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    this.UnitOfWork.Rollback();
                }

                return await Task.Run(() => res, cancellationToken);
            }
        }
    }
}
