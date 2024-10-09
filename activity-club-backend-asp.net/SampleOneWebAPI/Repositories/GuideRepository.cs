using Microsoft.EntityFrameworkCore;
using SampleOneWebAPI.Data;
using SampleOneWebAPI.Models;
using SampleOneWebAPI.Repositories.Interfaces;

namespace SampleOneWebAPI.Repositories
{
    public class GuideRepository: IGuideRepository
    {
        private readonly ActivityPortalDbContext _context;
        public GuideRepository(ActivityPortalDbContext context)
        {
            _context = context;
            
        }

        public async Task<List<Guide>> GetGuides()
        {
            return await _context.Guides.ToListAsync();
        }
        public async Task<bool> AddGuide(Guide guide)
        {
            await _context.Guides.AddAsync(guide);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task EditGuide(Guide guide)
        {
            var guideId = guide.Id;
            var guideToUpdate = await _context.Guides.FindAsync(guideId);
            guideToUpdate.Email = guide.Email;
            guideToUpdate.FullName = guide.FullName;
            guideToUpdate.EventGuides = guide.EventGuides;
            guideToUpdate.Password = guide.Password;
            guideToUpdate.Photo = guide.Photo;
            guideToUpdate.DateOfBirth = guide.DateOfBirth;
            guideToUpdate.JoiningDate = guide.JoiningDate;
            guideToUpdate.Profession = guide.Profession;
            await _context.SaveChangesAsync();
        }
    }
}
