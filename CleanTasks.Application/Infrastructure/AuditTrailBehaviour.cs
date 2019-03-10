using MediatR;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTasks.Application.Infrastructure
{
    public class AuditTrailBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _log;

        public AuditTrailBehaviour(ILogger log)
        {
            _log = log;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _log.Information("{@request}", request);
            return next();
        }
    }
}
