using CleanTasks.Application.BaseClasses;
using MediatR;

namespace CleanTasks.Application.TodoArea.Commands
{
    public class DeleteTodoAreaCommand : BaseCommand, IRequest
    {
        public int? TodoAreaId { get; set; }
    }
}
