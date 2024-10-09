using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEvents(int? category, string? status, string? date, bool onlyOwned, int memberId);
        Task<Event> GetEvent(int id);
        Task<IEnumerable<Event>> GetAllEvents();
        Task AddEvent(Event eventModel);
        Task<bool> CheckEventExists(int id);
        Task EditEvent(Event eventModel);
        Task DeleteEvent(int id);
    }
}
