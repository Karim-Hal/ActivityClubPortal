using Microsoft.EntityFrameworkCore;
using SampleOneWebAPI.Data;
using SampleOneWebAPI.Repositories.Interfaces;

namespace SampleOneWebAPI.Repositories
{
    public class RoleRepository: IRoleRepository
    {
        private readonly ActivityPortalDbContext _context;
        public RoleRepository(ActivityPortalDbContext context) {
            _context = context;
        }
    }
}
