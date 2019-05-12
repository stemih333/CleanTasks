using CleanTodoTasks.Application.BaseClasses;
using MediatR;

namespace CleanTodoTasks.Application.TodoArea.Commands
{
    public class CreateTodoAreaCommand : BaseCommand, IRequest<int>
    {
        public string Name { get; set; }
    }
}
