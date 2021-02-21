namespace TenderManagementSystem.API.IntegrationTests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using System.Data;
    using System.Data.SqlClient;
    using System.Net.Http;
    using TenderManagementSystem.Application.Common.Interfaces;
    using TenderManagementSystem.Domain.UnitOfWork;
    using TenderManagementSystem.Persistence.Constant;
    using TenderManagementSystem.Persistence.UnitOfWork;

    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        public HttpClient GetAnonymousClient()
        {
            return this.CreateClient();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IConfigConstants, ConfigConstants>();
                services.AddSingleton<IDbConnection>(provider => new SqlConnection(provider.GetService<IConfigConstants>().TestConnectionString));
                services.AddTransient<IUnitOfWork>(provider => new UnitOfWork(provider.GetService<IDbConnection>()));
            });
        }
    }
}
