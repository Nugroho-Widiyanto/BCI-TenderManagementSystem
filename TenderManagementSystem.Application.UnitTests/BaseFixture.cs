namespace TenderManagementSystem.Application.UnitTests
{
    using AutoMapper;
    using Common.Mappings;
    using System.Data;

    public class BaseFixture
    {
        public IMapper Mapper { get; }
        public IDbConnection DBConnection { get; }

        public BaseFixture()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            this.Mapper = configurationProvider.CreateMapper();
        }
    }
}
