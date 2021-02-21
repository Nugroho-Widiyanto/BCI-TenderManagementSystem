namespace TenderManagementSystem.Application.Common.Behaviors
{
    using Interfaces;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _stopwatch;
        private readonly ILogger<TRequest> _logger;
        private readonly IConfigConstants _configConstants;

        public PerformanceBehavior(ILogger<TRequest> logger, IConfigConstants constants)
        {
            _stopwatch = new Stopwatch();
            _logger = logger;
            _configConstants = constants;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _stopwatch.Start();
            var response = await next();
            _stopwatch.Stop();
            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;

            if (elapsedMilliseconds <= _configConstants.LongRunningProcessMilliseconds) return response;

            //todo:
            var userName = "Widi";
            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("Tender Management System Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userName, request);

            return response;
        }
    }
}
