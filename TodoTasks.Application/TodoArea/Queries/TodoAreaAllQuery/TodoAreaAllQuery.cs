using CleanTodoTasks.Application.TodoArea.Models;
using MediatR;
using System.Collections.Generic;

namespace CleanTodoTasks.Application.TodoArea.Queries
{
    public class TodoAreaAllQuery : IRequest<List<TodoAreaDto>>
    {
    }
}
