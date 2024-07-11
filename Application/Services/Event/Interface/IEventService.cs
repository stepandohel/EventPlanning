using Domain.Models;
using EventPlanning.Models.Event;
using EventPlanning.Services.Base;
using EventEntity = Domain.Models.Event;

namespace EventPlanning.Services.Event.Interface
{
    public interface IEventService : IService
    {
        Task<EventEntity> CreateEventAsync(EventServiceModel eventServiceModel, CancellationToken ct = default);
        Task<bool> DeleteEventByIdAsync(int eventId, string userId, CancellationToken ct = default);
        Task SubscribeToEvent(int eventId, User currentUser, CancellationToken ct = default);
        Task UnsubscribeToEvent(int eventId, User currentUser, CancellationToken ct = default);
        IEnumerable<EventEntity> GetEventsWithFields(CancellationToken ct = default);
        IEnumerable<EventEntity> GetUserEventsWithFields(string userId, CancellationToken ct = default);
    }
}
