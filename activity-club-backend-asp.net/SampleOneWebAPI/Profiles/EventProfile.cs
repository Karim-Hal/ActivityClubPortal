using AutoMapper;
using SampleOneWebAPI.DTOs.Event;
using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Profiles
{
    public class EventProfile: Profile
    {
        public EventProfile() { 
            CreateMap<Event, EventDTO>();
            CreateMap<EventDTO, Event>();
          
        }
    }
}
