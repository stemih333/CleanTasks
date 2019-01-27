using CleanTasks.Application.BaseClasses;
using MediatR;

namespace CleanTasks.Application.TodoArea.Commands
{
    public class CreateTodoAreaCommand : BaseCommand, IRequest<int>
    {
        public string Name { get; set; }
    }
}
