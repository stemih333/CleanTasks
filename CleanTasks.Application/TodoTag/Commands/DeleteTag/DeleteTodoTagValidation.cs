using FluentValidation;

namespace CleanTasks.Application.TodoTag.Commands
{
    public class DeleteTodoTagValidation : AbstractValidator<DeleteTodoTagCommand>
    {
        public DeleteTodoTagValidation()
        {
            RuleFor(_ => _.TagId).NotEmpty();
        }
    }
}
