using CleanTodoTasks.Application.BaseClasses;
using MediatR;

namespace CleanTodoTasks.Application.TodoTag.Commands
{
    public class DeleteTodoTagCommand : BaseCommand, IRequest
    {
        public int? TagId { get; set; }
    }
}
