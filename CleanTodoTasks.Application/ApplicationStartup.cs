using CleanTodoTasks.Application.Attachment.Commands.CreateAttachment;
using CleanTodoTasks.Application.Behaviours;
using CleanTodoTasks.DataAccess;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTodoTasks.Application
{
    public class ApplicationStartup
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddMediatR(typeof(CreateAttachmentHandler).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuditTrailBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            DataAccessStartup.ConfigureServices(services, connectionString);
        }
    }
}
