using CleanTasks.Application.BaseClasses;
using CleanTasks.Domain.Enums;
using MediatR;

namespace CleanTasks.Application.Todo.Commands
{
    public class CreateTodoCommand : BaseCommand, IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedToName { get; set; }

        public TodoTypes Type { get; set; }

        public int? TodoAreaId { get; set; }
    }
}
