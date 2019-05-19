using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace TodoTasks.Application.Behaviours
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
            _log.LogInformation("{@request}", request);
            return next();
        }
    }
}
