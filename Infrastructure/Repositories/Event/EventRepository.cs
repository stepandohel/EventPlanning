using Application.Repositories.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using EventEntity = Domain.Models.Event;

namespace EventPlanning.Repositories.Event
{
    public class EventRepository : BaseRepository<EventEntity>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<EventEntity> GetEventsWithFields()
        {
            var eventsWithFields = _context.Events.Include(x => x.Members)
                .Include(x => x.EventFields).ThenInclude(x => x.FieldDescription).AsEnumerable();

            return eventsWithFields;
        }

        public async Task<EventEntity> GetEventWithMembersAsync(int id)
        {
            var eventWithMembers = await _context.Set<EventEntity>().Include(x => x.Members).FirstOrDefaultAsync(x => x.Id.Equals(id));

            return eventWithMembers;
        }

        public IEnumerable<EventEntity> GetUserEventsWithFieldsAsync(string userId)
        {
            var eventsWithFields = _context.Events.Include(x => x.Members)
                .Include(x => x.EventFields).ThenInclude(x => x.FieldDescription)
                .Where(x => x.CreatorId.Equals(userId))
                .AsEnumerable();

            return eventsWithFields;
        }

        public async Task SubscribeToEvent(EventEntity eventEntityWithMembers, Domain.Models.User currentUser)
        {
            eventEntityWithMembers.Members.Add(currentUser);

            await _context.SaveChangesAsync();
        }

        public async Task UnsubscribeToEvent(EventEntity eventEntityWithMembers, Domain.Models.User currentUser)
        {
            eventEntityWithMembers.Members.Remove(currentUser);

            await _context.SaveChangesAsync();
        }
    }
}
