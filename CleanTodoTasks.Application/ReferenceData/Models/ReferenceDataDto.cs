using System.Collections.Generic;

namespace CleanTodoTasks.Application.ReferenceData.Models
{
    public class ReferenceDataDto
    {
        public IEnumerable<IdNameDto> Reasons { get; set; }
        public IEnumerable<IdNameDto> Statuses { get; set; }
        public IEnumerable<IdNameDto> Types { get; set; }
    }
}
