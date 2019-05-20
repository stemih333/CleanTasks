using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TodoTasks.Common;
using TodoTasks.Domain.Entities;

namespace TodoTasks.DataAccess
{
    public static class TestData
    {
        private const string User = "f928ad13-311f-4e9f-95b8-7914febf53ab";
        public static void Init(IServiceProvider serviceProvider)
        {
            using (var context = new TodoDbContext(serviceProvider.GetRequiredService<DbContextOptions<TodoDbContext>>())) {

                
                var area1 = new TodoArea
                {
                    Name = "In memory area 1"
                };

                var area2 = new TodoArea
                {
                    Name = "In memory area 2"
                };

                context.TodoAreas.AddRange(area1, area2);
                context.SaveAuditableChangesAsync(User);

                var permission1 = new TodoAreaPermission
                {
                    UserId = User,
                    TodoArea = area1
                };

                var permission2 = new TodoAreaPermission
                {
                    UserId = User,
                    TodoArea = area2
                };

                context.TodoAreaPermissions.AddRange(permission1, permission2);
                context.SaveAuditableChangesAsync(User);

                var todo1 = new Todo
                {
                    AssignedTo = User,
                    Comments = new List<TodoComment> { new TodoComment { Value = "In memory comment 1" } },
                    Description = "desc 1",
                    Notify = true,
                    Status = TodoStatus.New,
                    Title = "In memory todo 1",
                    Type = TodoType.Task,
                    TodoArea = area1,
                    Tags = new List<TodoTag> { new TodoTag { Value = "In memory tag 1" } }
                };

                var todo2 = new Todo
                {
                    Comments = new List<TodoComment> { new TodoComment { Value = "In memory comment 2" } },
                    Description = "desc 2",
                    Notify = true,
                    Status = TodoStatus.New,
                    Title = "In memory todo 2",
                    Type = TodoType.Task,
                    TodoArea = area1,
                    Tags = new List<TodoTag> { new TodoTag { Value = "In memory tag 2" } }
                };

                context.Todos.AddRange(todo1, todo2);
                context.SaveAuditableChangesAsync(User);
            }          
        }
    }
}
