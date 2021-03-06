﻿using FluentValidation;

namespace TodoTasks.Application.TodoArea.Commands
{
    public class CreateTodoAreaValidation : AbstractValidator<CreateTodoAreaCommand>
    {
        public CreateTodoAreaValidation()
        {
            RuleFor(_ => _.Name).NotEmpty().MaximumLength(50).MinimumLength(2);
        }
    }
}
