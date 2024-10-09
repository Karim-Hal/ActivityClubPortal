using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleOneWebAPI.DTOs.Event;
using SampleOneWebAPI.Models;
using SampleOneWebAPI.Services.Interfaces;

namespace SampleOneWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMemberService _memberService;
        
        private readonly IMapper _mapper;

        public EventController(IEventService eventService,IMemberService memberService, IMapper mapper)
        {
            _eventService = eventService;
            _memberService = memberService;
            _mapper = mapper;
        }

        [HttpGet("GetEvents/{category?}/{status?}/{date?}/{onlyOwned}/{email}")]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetEvents(int? category, string? status, string? date, bool onlyOwned, string email)
        {
            try
            {
                var memberId = await _memberService.GetMemberId(email);
                var events = await _eventService.GetEvents(category, status, date, onlyOwned, memberId);
               
                var eventDTOs = _mapper.Map<List<EventDTO>>(events);
                return Ok(eventDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllEvents")]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEvents();
            var eventDTOs = _mapper.Map<IEnumerable<EventDTO>>(events);
            return Ok(eventDTOs);
        }

        [HttpPost("AddEvent")]
        public async Task<ActionResult> AddEvent(EventDTO eventDTO)
        {
            try
            {
                var eventModel = _mapper.Map<Event>(eventDTO);
                if (eventModel.Id == 0)
                {
                    await _eventService.AddEvent(eventModel);
                }
                else
                {
                    var checkIfExists = await _eventService.CheckEventExists(eventModel.Id);
                    if (checkIfExists)
                    {
                        await _eventService.EditEvent(eventModel);
                    }
                    else
                    {
                        return NotFound("Event not found");
                    }
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetEvent/{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            try
            {
                if (await _eventService.CheckEventExists(id) is false)
                {
                    return NotFound($"Event with id {id} not found!");
                }
                var eventModel = await _eventService.GetEvent(id);
                return Ok(eventModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteEvent/{id}")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            try
            {
                if (await _eventService.CheckEventExists(id))
                {
                    await _eventService.DeleteEvent(id);
                    return Ok("Event Deleted Successfully!");
                }
                else
                {
                    return NotFound($"Event with id {id} does not exist");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RegisterEvent/{email}/{eventId}")]
        public async Task<ActionResult<bool>> RegisterEvent(string email, int eventId)
        {
            var memberId = await _memberService.GetMemberId(email);
            var checkIfRegistered = await _memberService.CheckRegisteredEvent(memberId, eventId);
            if (checkIfRegistered)
            {
                return Ok(false);
            }

            var registered = await _memberService.RegisterEvent(memberId, eventId);
            return Ok(registered);
            

        }
        

    }

}
