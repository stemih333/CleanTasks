﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTasks.Application.Todo.Models
{
    public class PagedTodoResultDto
    {
        public List<TodoDto> Todos { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortOrder { get; set; }
        public string SortProperty { get; set; }
    }
}
