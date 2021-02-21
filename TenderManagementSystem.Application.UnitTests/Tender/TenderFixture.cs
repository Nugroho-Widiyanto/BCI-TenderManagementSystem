using System;

namespace TenderManagementSystem.Application.UnitTests.Tender
{
    using Common.Interfaces;
    using Domain.Repositories;
    using Domain.UnitOfWork;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class TenderFixture : BaseFixture
    {
        public IConfigConstants ConfigConstants { get; }
        public IUnitOfWork UnitOfWork { get; }

        public TenderFixture()
        {
            var mockConstant = new Mock<IConfigConstants>();
            mockConstant.SetupGet(p => p.INVALID_TENDER_ID).Returns("Tender Id is required!");
            mockConstant.SetupGet(p => p.INVALID_TENDER_NAME).Returns("Tender Name is required!");
            mockConstant.SetupGet(p => p.INVALID_TENDER_CONTRACT_NUMBER).Returns("Contract Number is required!");
            ConfigConstants = mockConstant.Object;

            var mockRepository = new Mock<ITenderRepository>();
            MockAdd(mockRepository);
            MockUpdate(mockRepository);
            MockDelete(mockRepository);
            MockGetAll(mockRepository);
            MockGetOne(mockRepository);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(repo => repo.Tenders).Returns(mockRepository.Object);
            UnitOfWork = mockUnitOfWork.Object;
        }

        private Mock<ITenderRepository> MockAdd(Mock<ITenderRepository> mockRepository)
        {
            mockRepository.Setup(p => p.Add(It.IsAny<Domain.Entities.Tender>())).Returns(Task.Run(() => 100));
            return mockRepository;
        }

        private Mock<ITenderRepository> MockUpdate(Mock<ITenderRepository> mockRepository)
        {
            mockRepository.Setup(p => p.Update(It.IsAny<Domain.Entities.Tender>())).Returns(Task.Run(() => true));
            return mockRepository;
        }

        private Mock<ITenderRepository> MockDelete(Mock<ITenderRepository> mockRepository)
        {
            mockRepository.Setup(p => p.Delete(100)).Returns(Task.Run(() => true));
            return mockRepository;
        }

        private Mock<ITenderRepository> MockGetAll(Mock<ITenderRepository> mockRepository)
        {
            mockRepository.Setup(p => p.GetAll()).Returns(Task.Run(() => GetTenderList()));
            return mockRepository;
        }

        private Mock<ITenderRepository> MockGetOne(Mock<ITenderRepository> mockRepository)
        {
            mockRepository.Setup(p => p.GetOne(100)).Returns(Task.Run(() => GetTenderList().FirstOrDefault(u => u.Id == 100)));
            mockRepository.Setup(p => p.GetOne(110)).Returns(Task.Run(() => GetTenderList().FirstOrDefault(u => u.Id == 110)));
            return mockRepository;
        }

        private static IEnumerable<Domain.Entities.Tender> GetTenderList()
        {
            return new List<Domain.Entities.Tender>
            {
                new Domain.Entities.Tender
                {
                    Id = 100,
                    Name = "Tender Name 100",
                    ContractNumber = "Contract Number 100",
                    ReleaseDate = new DateTime(2021, 01, 01),
                    ClosingDate = new DateTime(2021, 02, 01),
                    Description = "Tender Description 100",
                },
                new Domain.Entities.Tender
                {
                    Id = 110,
                    Name = "Tender Name 110",
                    ContractNumber = "Contract Number 110",
                    ReleaseDate = new System.DateTime(2021, 03, 01),
                    ClosingDate = new System.DateTime(2021, 04, 01),
                    Description = "Tender Description 110",
                }
            };
        }

        [CollectionDefinition("TenderCollection")]
        public class QueryCollection : ICollectionFixture<TenderFixture>
        {
        }
    }
}