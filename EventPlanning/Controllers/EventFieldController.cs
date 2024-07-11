using AutoMapper;
using EventPlanning.Models.FieldDescription;
using EventPlanning.Services.EventField.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventPlanning.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventFieldController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IEventFieldDescriptionService _eventFieldService;
        private readonly IMapper _mapper;

        public EventFieldController(ILogger<EventsController> logger, IEventFieldDescriptionService eventFieldService, IMapper mapper)
        {
            _logger = logger;
            _eventFieldService = eventFieldService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<FieldDescriptionViewModel>> CreateEventField(FieldDescriptionViewModel fieldDescriptionViewModel)
        {
            var eventFieldControllerModel = _mapper.Map<FieldDescriptionServiceModel>(fieldDescriptionViewModel);
            var currentUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            eventFieldControllerModel.CreatorId = currentUserId;

            var newFieldDescription = await _eventFieldService.CreateEventFieldAsync(eventFieldControllerModel);

            return Ok(_mapper.Map<FieldDescriptionViewModel>(newFieldDescription));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<FieldDescriptionViewModel>>> GetEventFields()
        {
            var currentUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var eventFileds = _eventFieldService.GetEventFields(currentUserId);

            return Ok(_mapper.Map<IEnumerable<FieldDescriptionViewModel>>(eventFileds));
        }
    }
}
