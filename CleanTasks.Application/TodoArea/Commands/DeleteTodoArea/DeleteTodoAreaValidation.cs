using FluentValidation;

namespace CleanTasks.Application.TodoArea.Commands
{
    public class DeleteTodoAreaValidation : AbstractValidator<DeleteTodoAreaCommand>
    {
        public DeleteTodoAreaValidation()
        {
            RuleFor(_ => _.TodoAreaId).NotEmpty();
        }
    }
}
