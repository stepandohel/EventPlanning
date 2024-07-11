using AutoMapper;
using Domain.Models;
using EventPlanning.Models.FieldDescription;
using EventPlanning.Repositories.Interfaces;
using EventPlanning.Services.EventField.Interface;

namespace EventPlanning.Services.EventField
{
    public class EventFieldDescriptionService : IEventFieldDescriptionService
    {
        private readonly IBaseRepository<FieldDescription> _eventFieldRepository;
        private readonly IMapper _mapper;

        public EventFieldDescriptionService(IBaseRepository<FieldDescription> eventFieldRepository, IMapper mapper)
        {
            _eventFieldRepository = eventFieldRepository;
            _mapper = mapper;
        }

        public async Task<FieldDescription> CreateEventFieldAsync(FieldDescriptionServiceModel fieldDescriptionServiceModel, CancellationToken ct = default)
        {
            var newEventField = _mapper.Map<FieldDescription>(fieldDescriptionServiceModel);
            var createdEventFieldId = await _eventFieldRepository.CreateItemAsync(newEventField);

            return createdEventFieldId;
        }

        public IEnumerable<FieldDescription> GetEventFields(string userId, CancellationToken ct = default)
        {
            var eventFields = _eventFieldRepository.GetItems(ct).Where(x => x.CreatorId.Equals(userId));

            return eventFields;
        }
    }
}
