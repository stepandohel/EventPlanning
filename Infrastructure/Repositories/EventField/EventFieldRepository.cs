using Application.Repositories.Interfaces;
using Domain;
using EventFieldEntity = Domain.Models.EventField;

namespace EventPlanning.Repositories.EventField
{
    public class EventFieldRepository : BaseRepository<EventFieldEntity>, IEventFieldRepository
    {
        public EventFieldRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<EventFieldEntity>> CreateEventFieldsAsync(IEnumerable<EventFieldEntity> eventFields)
        {
            await _context.Set<EventFieldEntity>().AddRangeAsync(eventFields);

            return eventFields;
        }

        public async Task UpdateEventFieldsAsync(IEnumerable<EventFieldEntity> eventFields)
        {
            _context.Set<EventFieldEntity>().UpdateRange(eventFields);
            await _context.SaveChangesAsync();
        }
    }
}

