using Domain.Models.Base;

namespace Domain.Models
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public int MemberCount { get; set; }
        public DateTime DateTime { get; set; }

        public string CreatorId { get; set; }
        public User Creator { get; set; }

        public ICollection<User> Members { get; set; }

        public ICollection<UserEvent> UserEvents { get; set; }
        public ICollection<EventField> EventFields { get; set; }
    }
}
