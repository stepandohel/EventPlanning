using Domain.Models;
using EventPlanning.Repositories.Interfaces;
using EventEntity = Domain.Models.Event;

namespace Application.Repositories.Interfaces
{
    public interface IEventRepository : IBaseRepository<EventEntity>
    {
        Task<EventEntity> GetEventWithMembersAsync(int id);
        Task SubscribeToEvent(EventEntity eventEntityWithMembers, User currentUser);
        Task UnsubscribeToEvent(EventEntity eventEntityWithMembers, User currentUser);
        IEnumerable<EventEntity> GetEventsWithFields();
        IEnumerable<EventEntity> GetUserEventsWithFieldsAsync(string userId);
    }
}
