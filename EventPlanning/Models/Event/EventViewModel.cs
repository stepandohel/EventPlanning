using EventPlanning.Models.EventField;
using EventPlanning.Models.User;

namespace EventPlanning.Models.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MemberCount { get; set; }
        public DateTime DateTime { get; set; }
        public int CurrentMemberCount { get; set; }
        public UserViewModel Creator { get; set; }
        public bool IsRegistered { get; set; }
        public ICollection<UserViewModel> Members { get; set; }
        public ICollection<EventFieldWithValueViewModel> EventFields { get; set; }
    }
}
