namespace TenderManagementSystem.Persistence.Constant
{
    using Application.Common.Interfaces;
    using Microsoft.Extensions.Configuration;

    public class ConfigConstants : IConfigConstants
    {
        private IConfiguration Configuration { get; }

        public ConfigConstants(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string MainConnectionString => this.Configuration.GetConnectionString(nameof(MainConnectionString));
        public string TestConnectionString => this.Configuration.GetConnectionString(nameof(TestConnectionString));
        public int LongRunningProcessMilliseconds => int.Parse(this.Configuration["AppSettings:LongRunningProcessMilliseconds"]);
        public string INVALID_TENDER_ID => this.Configuration["ErrorMessages:INVALID_TENDER_ID"];
        public string INVALID_TENDER_NAME => this.Configuration["ErrorMessages:INVALID_TENDER_NAME"];
        public string INVALID_TENDER_CONTRACT_NUMBER => this.Configuration["ErrorMessages:INVALID_TENDER_CONTRACT_NUMBER"];
    }
}
