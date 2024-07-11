using EventPlanning.Repositories.Interfaces;
using EventFieldEntity = Domain.Models.EventField;

namespace Application.Repositories.Interfaces
{
    public interface IEventFieldRepository : IBaseRepository<EventFieldEntity>
    {
        Task<IEnumerable<EventFieldEntity>> CreateEventFieldsAsync(IEnumerable<EventFieldEntity> eventFields);
        Task UpdateEventFieldsAsync(IEnumerable<EventFieldEntity> eventFields);
    }
}
