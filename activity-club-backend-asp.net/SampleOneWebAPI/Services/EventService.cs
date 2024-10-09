using SampleOneWebAPI.Models;
using SampleOneWebAPI.Repositories.Interfaces;
using SampleOneWebAPI.Services.Interfaces;

namespace SampleOneWebAPI.Services
{
    public class EventService: IEventService
    {
        private readonly IRepository<Event> _repository;
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository, IRepository<Event> repository)
        {
            _eventRepository = eventRepository;
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> GetEvents(int? category, string? status, string? date, bool onlyOwned, int memberId)
        {
            var events = await _eventRepository.GetEvents(category, status, date, onlyOwned, memberId);
            return events;
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _eventRepository.GetAllEvents();
        }
        public async Task<Event> GetEvent(int id)
        {
            var oneEvent = await _repository.GetById(id);
            return oneEvent;
        }

        public async Task AddEvent(Event e)
        {
            await _repository.Add(e);
        }

        public async Task<bool> CheckEventExists(int id)
        {
            return await _repository.CheckEntityExists(id);
        }

        public async Task EditEvent(Event e)
        {

            await _eventRepository.EditEvent(e);
        }

        public async Task DeleteEvent(int id)
        {
            await _repository.Delete(id);
        }
    }
}
