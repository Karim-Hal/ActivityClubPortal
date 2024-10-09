using SampleOneWebAPI.Data;
using SampleOneWebAPI.Repositories.Interfaces;

namespace SampleOneWebAPI.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {

        private readonly ActivityPortalDbContext _context;
        public CategoryRepository(ActivityPortalDbContext context)
        {
            _context = context;
            
        }
    }
}
