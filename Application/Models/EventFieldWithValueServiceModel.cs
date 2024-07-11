namespace EventPlanning.Models.EventField
{
    public class EventFieldWithValueServiceModel
    {
        public int EventId { get; set; }
        public int FieldDescriptionId { get; set; }

        public string FieldValue { get; set; }
    }
}
