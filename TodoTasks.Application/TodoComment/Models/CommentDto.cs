using System;

namespace TodoTasks.Application.TodoComment.Models
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string Value { get; set; }
        public int? TodoId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Created { get; set; }
    }
}
