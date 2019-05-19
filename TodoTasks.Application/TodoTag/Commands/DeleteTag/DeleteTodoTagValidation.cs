using FluentValidation;

namespace TodoTasks.Application.TodoTag.Commands
{
    public class DeleteTodoTagValidation : AbstractValidator<DeleteTodoTagCommand>
    {
        public DeleteTodoTagValidation()
        {
            RuleFor(_ => _.TagId).NotEmpty();
        }
    }
}
