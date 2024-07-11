using EventPlanning.Models.FieldDescription;

namespace EventPlanning.Models.EventField
{
    public class EventFieldWithValueViewModel
    {
        public int EventId { get; set; }

        public int FieldDescriptionId { get; set; }
        public FieldDescriptionViewModel FieldDescription { get; set; }

        public string FieldValue { get; set; }
    }
}
