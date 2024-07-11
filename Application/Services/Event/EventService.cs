using Application.Repositories.Interfaces;
using AutoMapper;
using Domain;
using Domain.Models;
using EventPlanning.Models.Event;
using EventPlanning.Models.EventField;
using EventPlanning.Models.FieldDescription;
using EventPlanning.Services.Event.Interface;
using EventEntity = Domain.Models.Event;
using EventFieldEntity = Domain.Models.EventField;

namespace EventPlanning.Services.Event
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventFieldRepository _eventFieldRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public EventService(IEventRepository eventRepository, IMapper mapper, IEventFieldRepository eventFieldRepository, ApplicationDbContext dbContext)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _eventFieldRepository = eventFieldRepository;
            _dbContext = dbContext;
        }
        public async Task<EventEntity> CreateEventAsync(EventServiceModel eventCreateModel, CancellationToken ct = default)
        {
            var newEvent = _mapper.Map<EventEntity>(eventCreateModel);

            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);

                var newEventEntity = await _eventRepository.CreateItemAsync(newEvent, ct);
                var eventFields = await CreateEventFieldsAsync(eventCreateModel.EventFields, newEventEntity.Id, ct);
                newEvent.EventFields = eventFields.ToList();
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync(ct);

                return newEventEntity;
            }
            catch (Exception)
            {
                await _dbContext.Database.RollbackTransactionAsync(ct);
                throw;
            }
        }

        public async Task<bool> DeleteEventByIdAsync(int eventId, string userId, CancellationToken ct = default)
        {
            var eventForDelete = await _eventRepository.GetItemByIdAsync(eventId, ct);
            if (eventForDelete != null && eventForDelete.CreatorId.Equals(userId))
            {
                await _eventRepository.DeleteItemByIdAsync(eventId, ct);

                return true;
            }

            return false;
        }

        public IEnumerable<EventEntity> GetEventsWithFields(CancellationToken ct = default)
        {
            var events = _eventRepository.GetEventsWithFields();

            return events;
        }

        public IEnumerable<EventEntity> GetUserEventsWithFields(string userId, CancellationToken ct = default)
        {
            var events = _eventRepository.GetUserEventsWithFieldsAsync(userId);

            return events;
        }

        public async Task SubscribeToEvent(int eventId, User currentUser, CancellationToken ct = default)
        {
            var eventWithMembers = await _eventRepository.GetEventWithMembersAsync(eventId);
            if (eventWithMembers.Members.Count < eventWithMembers.MemberCount && !eventWithMembers.Members.Contains(currentUser))
            {
                await _eventRepository.SubscribeToEvent(eventWithMembers, currentUser);
            }
        }

        public async Task UnsubscribeToEvent(int eventId, User currentUser, CancellationToken ct = default)
        {
            var eventWithMembers = await _eventRepository.GetEventWithMembersAsync(eventId);
            if (eventWithMembers.Members.Contains(currentUser))
            {
                await _eventRepository.UnsubscribeToEvent(eventWithMembers, currentUser);
            }
        }

        private async Task<IEnumerable<EventFieldEntity>> CreateEventFieldsAsync(IEnumerable<EventFieldWithValueServiceModel> eventFieldWithValueControllerModel, int eventId, CancellationToken ct = default)
        {
            var eventFields = _mapper.Map<IEnumerable<EventFieldEntity>>(eventFieldWithValueControllerModel);
            var newEventFields = await _eventFieldRepository.CreateEventFieldsAsync(eventFields);

            return newEventFields;
        }
    }
}
