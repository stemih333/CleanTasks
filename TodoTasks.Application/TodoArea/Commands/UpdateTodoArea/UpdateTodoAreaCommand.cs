using CleanTodoTasks.Application.BaseClasses;
using MediatR;

namespace CleanTodoTasks.Application.TodoArea.Commands
{
    public class UpdateTodoAreaCommand : BaseCommand, IRequest
    {
        public int? TodoAreaId { get; set; }
        public string Name { get; set; }
    }
}
