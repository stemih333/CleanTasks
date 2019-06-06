using Microsoft.AspNetCore.Http;

namespace TodoTasks.WebAPI.Models.InputModels
{
    public class AttachmentInputModel
    {
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string UserId { get; set; }
        public string FilePath { get; set; }
        public int TodoId { get; set; }
    }
}
