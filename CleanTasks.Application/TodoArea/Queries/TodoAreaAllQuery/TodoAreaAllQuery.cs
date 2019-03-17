using CleanTasks.Application.TodoArea.Models;
using MediatR;
using System.Collections.Generic;

namespace CleanTasks.Application.TodoArea.Queries
{
    public class TodoAreaAllQuery : IRequest<List<TodoAreaDto>>
    {
    }
}
