using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Event> EventsMemberships { get; set; }

        public ICollection<UserEvent> UserEvents { get; set; }

        public ICollection<FieldDescription> FieldDescriptions { get; set; }
    }
}
