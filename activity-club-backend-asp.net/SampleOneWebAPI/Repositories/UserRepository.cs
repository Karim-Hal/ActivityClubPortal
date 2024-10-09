
using Microsoft.EntityFrameworkCore;
using SampleOneWebAPI.Data;
using SampleOneWebAPI.DTOs.UserDTOs;
using SampleOneWebAPI.Repositories.Interfaces;

namespace SampleOneWebAPI.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly ActivityPortalDbContext _context;
        public UserRepository(ActivityPortalDbContext context)
        {
            _context = context;
            
        }

        public async Task<bool> LoginUser(UserDTO user)
        {
            var valid = await _context.Users.FirstOrDefaultAsync(u => u.Email== user.Email && u.Password == user.Password);
            return valid is not null;
        }

        //public async Task<bool> LoginRequest(LoginRequest loginRequest)
        //{
        //    var validLogin = await _context.Users.AnyAsync(u => u.Email ==  loginRequest.Email && u.Password == loginRequest.Password);
        //    return validLogin;

        //}
    }
}
