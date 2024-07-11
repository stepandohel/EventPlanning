using Domain.Models.Base;

namespace Domain.Models
{
    public class EventField : BaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int FieldDescriptionId { get; set; }
        public FieldDescription FieldDescription { get; set; }

        public string FieldValue { get; set; }
    }
}
