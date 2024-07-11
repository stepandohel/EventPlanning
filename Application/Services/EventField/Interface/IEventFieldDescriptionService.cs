using Domain.Models;
using EventPlanning.Models.FieldDescription;
using EventPlanning.Services.Base;

namespace EventPlanning.Services.EventField.Interface
{
    public interface IEventFieldDescriptionService : IService
    {
        Task<FieldDescription> CreateEventFieldAsync(FieldDescriptionServiceModel fieldDescriptionServiceModel, CancellationToken ct = default);
        IEnumerable<FieldDescription> GetEventFields(string userId, CancellationToken ct = default);
    }
}
