using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Repositories.Interfaces
{
    public interface IGuideRepository
    {
        Task<List<Guide>> GetGuides();
        Task<bool> AddGuide(Guide guide);
        Task EditGuide(Guide guide);
    }
}
