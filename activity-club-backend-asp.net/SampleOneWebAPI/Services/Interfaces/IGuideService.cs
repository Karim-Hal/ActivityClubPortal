using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Services.Interfaces
{
    public interface IGuideService
    {
        Task<IEnumerable<Guide>> GetGuides();
        Task<Guide> GetGuide(int id);
        Task<bool> AddGuide(Guide guide);
        Task<bool> CheckGuideExists(int id);
        Task EditGuide(Guide guide);
        Task DeleteGuide(int id);
    }
}
