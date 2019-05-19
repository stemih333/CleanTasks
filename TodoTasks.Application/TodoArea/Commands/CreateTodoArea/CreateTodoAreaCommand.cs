using TodoTasks.Application.BaseClasses;
using MediatR;

namespace TodoTasks.Application.TodoArea.Commands
{
    public class CreateTodoAreaCommand : BaseCommand, IRequest<int>
    {
        public string Name { get; set; }
    }
}
