using TodoTasks.Application.TodoArea.Models;
using MediatR;
using System.Collections.Generic;

namespace TodoTasks.Application.TodoArea.Queries
{
    public class TodoAreaAllQuery : IRequest<List<TodoAreaDto>>
    {
    }
}
