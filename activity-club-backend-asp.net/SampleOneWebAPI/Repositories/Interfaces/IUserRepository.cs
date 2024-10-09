
using SampleOneWebAPI.DTOs.UserDTOs;

namespace SampleOneWebAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        //public Task<bool> LoginRequest(LoginRequest loginRequest);
        Task<bool> LoginUser(UserDTO user);
    }
}
