﻿using CleanTodoTasks.DataAccess;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.Todo.Commands.EditTodo
{
    public class EditTodoHandler : IRequestHandler<EditTodoCommand, int>
    {
        private readonly TodoDbContext _todoDbContext;

        public EditTodoHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<int> Handle(EditTodoCommand request, CancellationToken cancellationToken)
        {
            var todoToEdit = _todoDbContext.Todos.FirstOrDefault(_ => _.TodoId == request.TodoId);

            if (todoToEdit == null) throw new NullReferenceException("No todo task found with ID: " + request.TodoId);

            todoToEdit.Title = request.Title;
            todoToEdit.Description = request.Description;
            todoToEdit.AssignedTo = request.AssignedTo;
            todoToEdit.Type = request.Type.Value;
            todoToEdit.CloseReason = request.CloseReasonId;
            todoToEdit.Status = request.TodoStatusId.Value;
            todoToEdit.Notify = request.Notify;

            _todoDbContext.Update(todoToEdit);
           
            return await _todoDbContext.SaveAuditableChangesAsync(request.UserId);
        }
    }
}
