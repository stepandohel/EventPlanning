using EventPlanning.Models.EventField;
using EventPlanning.Models.User;

namespace EventPlanning.Models.Event
{
    public class EventServiceModel
    {
        public string Name { get; set; }
        public int MemberCount { get; set; }
        public DateTime DateTime { get; set; }

        public string CreatorId { get; set; }

        public IEnumerable<UserServiceModel> Members { get; set; }
        public IEnumerable<EventFieldWithValueServiceModel> EventFields { get; set; }
    }
}
