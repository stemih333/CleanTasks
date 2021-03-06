﻿using TodoTasks.Application.BaseClasses;
using MediatR;

namespace TodoTasks.Application.TodoTag.Commands
{
    public class DeleteTodoTagCommand : BaseCommand, IRequest
    {
        public int? TagId { get; set; }
    }
}
