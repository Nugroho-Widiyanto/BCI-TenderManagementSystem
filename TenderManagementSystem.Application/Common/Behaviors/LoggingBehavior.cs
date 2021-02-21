namespace TenderManagementSystem.Application.Common.Behaviors
{
    using MediatR.Pipeline;
    using Microsoft.Extensions.Logging;
    using System.Threading;
    using System.Threading.Tasks;

    public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {

            //todo:
            var userName = "Widi";
            var requestName = typeof(TRequest).Name;

            await Task.Run(() => _logger.LogInformation("Tender Management System Request: {Name} {@UserName} {@Request}",
                requestName, userName, request), cancellationToken);
        }
    }
}
