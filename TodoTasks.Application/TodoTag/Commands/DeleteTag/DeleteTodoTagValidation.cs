using FluentValidation;

namespace CleanTodoTasks.Application.TodoTag.Commands
{
    public class DeleteTodoTagValidation : AbstractValidator<DeleteTodoTagCommand>
    {
        public DeleteTodoTagValidation()
        {
            RuleFor(_ => _.TagId).NotEmpty();
        }
    }
}
