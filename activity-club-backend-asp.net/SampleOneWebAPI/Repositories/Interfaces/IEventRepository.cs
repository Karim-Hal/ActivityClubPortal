using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<List<Event>> GetEvents(int? category, string? status,string? date, bool onlyOwned, int memberId);
        Task<IEnumerable<Event>> GetAllEvents();
        Task EditEvent(Event eventModel);
    }
}
