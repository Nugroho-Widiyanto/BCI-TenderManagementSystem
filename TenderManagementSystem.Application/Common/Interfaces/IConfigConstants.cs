namespace TenderManagementSystem.Application.Common.Interfaces
{
    public interface IConfigConstants
    {
        string MainConnectionString { get; }
        string TestConnectionString { get; }
        int LongRunningProcessMilliseconds { get; }
        string INVALID_TENDER_ID { get; }
        string INVALID_TENDER_NAME { get; }
        string INVALID_TENDER_CONTRACT_NUMBER { get; }
    }
}
