using FluentValidation;

namespace TodoTasks.Application.TodoArea.Commands
{
    class UpdateTodoAreaValidation : AbstractValidator<UpdateTodoAreaCommand>
    {
        public UpdateTodoAreaValidation()
        {
            RuleFor(_ => _.TodoAreaId).NotEmpty();
            RuleFor(_ => _.Name).NotEmpty().MaximumLength(50);
        }
    }
}
