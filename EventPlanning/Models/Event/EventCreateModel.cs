using EventPlanning.Models.EventField;

namespace EventPlanning.Models.Event
{
    public class EventCreateModel
    {
        public string Name { get; set; }
        public int MemberCount { get; set; }
        public string DateTime { get; set; }

        public EventFieldCreateModel[] EventFields { get; set; }
    }
}
