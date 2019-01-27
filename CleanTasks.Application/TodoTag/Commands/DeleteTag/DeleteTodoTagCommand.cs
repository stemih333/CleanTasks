using CleanTasks.Application.BaseClasses;
using MediatR;

namespace CleanTasks.Application.TodoTag.Commands
{
    public class DeleteTodoTagCommand : BaseCommand, IRequest
    {
        public int? TagId { get; set; }
    }
}
