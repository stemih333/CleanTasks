using CleanTodoTasks.Application.BaseClasses;
using MediatR;

namespace CleanTodoTasks.Application.TodoArea.Commands
{
    public class DeleteTodoAreaCommand : BaseCommand, IRequest
    {
        public int? TodoAreaId { get; set; }
    }
}
