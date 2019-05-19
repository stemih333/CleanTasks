using TodoTasks.Application.Attachment.Commands.CreateAttachment;
using TodoTasks.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TodoTasks.Application
{
    public class ApplicationStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateAttachmentHandler).Assembly);
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuditTrailBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));           
        }
    }
}
