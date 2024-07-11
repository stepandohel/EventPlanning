using AutoMapper;
using Domain.Models;
using EventPlanning.Models.Event;
using EventPlanning.Services.Event.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventPlanning.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        private const string MissMatchIdError = "Cannot get other's user events ";

        public EventsController(ILogger<EventsController> logger, IEventService eventService, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _eventService = eventService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<EventViewModel> GetEvents()
        {
            var events = _eventService.GetEventsWithFields();

            //TODO нормально сделать
            var eventsViewModels = _mapper.Map<IEnumerable<EventViewModel>>(events);
            var currentUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            foreach (var item in eventsViewModels)
            {
                if (item.Members.Any(x => x.Id.Equals(currentUserId)))
                {
                    item.IsRegistered = true;
                }
            }

            return Ok(eventsViewModels);
        }

        [HttpGet("/api/MyEvents")]
        [Authorize]
        public ActionResult<EventViewModel> GetUserEvents()
        {
            var currentUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var events = _eventService.GetUserEventsWithFields(currentUserId);

            return Ok(_mapper.Map<IEnumerable<EventViewModel>>(events));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent(EventCreateModel eventCreateModel)
        {
            var currentUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var eventServiceModel = _mapper.Map<EventServiceModel>(eventCreateModel);
            eventServiceModel.DateTime = Convert.ToDateTime(eventCreateModel.DateTime);
            eventServiceModel.CreatorId = currentUserId;
            var newEvent = await _eventService.CreateEventAsync(eventServiceModel);

            return Created();
        }

        [HttpPost("Subscribe")]
        [Authorize]
        public async Task<IActionResult> SubscribeToEvent(int eventId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            await _eventService.SubscribeToEvent(eventId, currentUser);

            return Ok();
        }

        [HttpPost("Unsubscribe")]
        [Authorize]
        public async Task<IActionResult> UnsubscribeToEvent(int eventId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            await _eventService.UnsubscribeToEvent(eventId, currentUser);

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            var currentUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var isSuccess = await _eventService.DeleteEventByIdAsync(eventId, currentUserId);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
