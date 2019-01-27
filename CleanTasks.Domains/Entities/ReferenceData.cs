
using CleanTasks.Domain.Enums;

namespace CleanTasks.Domain.Entities
{
    public class ReferenceData : AuditableEntity
    {
        public int ReferenceDataId { get; set; }
        public string Name { get; set; }
        public ReferenceDataTypes ReferenceDataType { get; set; }

        public int? TodoAreaId { get; set; }
        public TodoArea TodoArea { get; set; }
    }
}
