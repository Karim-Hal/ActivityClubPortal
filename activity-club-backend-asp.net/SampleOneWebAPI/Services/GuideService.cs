using SampleOneWebAPI.Models;
using SampleOneWebAPI.Repositories.Interfaces;
using SampleOneWebAPI.Services.Interfaces;

namespace SampleOneWebAPI.Services
{
    public class GuideService: IGuideService
    {
        private readonly IRepository<Guide> _repository;
        private readonly IGuideRepository _guideRepository;
        public GuideService(IGuideRepository guideRepository, IRepository<Guide> repository)
        {
            _repository = repository;
            _guideRepository = guideRepository;
            
        }
        public async Task<IEnumerable<Guide>> GetGuides()
        {
            var guides = await _guideRepository.GetGuides();
            return guides;
        }

        public async Task<Guide> GetGuide(int id)
        {
            var guide = await _repository.GetById(id);
            return guide;
        }

        public async Task<bool> AddGuide(Guide guide)
        {
            return await _guideRepository.AddGuide(guide);
            
        }

        public async Task<bool> CheckGuideExists(int id)
        {
            return await _repository.CheckEntityExists(id);
        }

        public async Task EditGuide(Guide guide)
        {
            await _guideRepository.EditGuide(guide);
        }

        public async Task DeleteGuide(int id)
        {
            await _repository.Delete(id);
        }
    }
}
