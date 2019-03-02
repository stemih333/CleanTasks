﻿using CleanTasks.Application.BaseClasses;
using MediatR;

namespace CleanTasks.Application.TodoArea.Commands
{
    public class UpdateTodoAreaCommand : BaseCommand, IRequest
    {
        public int? TodoAreaId { get; set; }
        public string Name { get; set; }
    }
}