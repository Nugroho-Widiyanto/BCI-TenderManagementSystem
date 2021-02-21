namespace TenderManagementSystem.Persistence
{
    using Application.Common.Interfaces;
    using Constant;
    using Domain.UnitOfWork;
    using Microsoft.Extensions.DependencyInjection;
    using System.Data;
    using System.Data.SqlClient;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddSingleton<IConfigConstants, ConfigConstants>();
            services.AddSingleton<IDbConnection>(conn => new SqlConnection(conn.GetService<IConfigConstants>().MainConnectionString));
            services.AddTransient<IUnitOfWork>(uof => new UnitOfWork.UnitOfWork(uof.GetService<IDbConnection>()));
            return services;
        }
    }
}
