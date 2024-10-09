using SampleOneWebAPI.DTOs.UserDTOs;
using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task AddUser(UserDTO user);
        Task<bool> CheckUserExists(int id);
        Task EditUser(UserDTO user);
        Task DeleteUser(int id);
        Task<bool> LoginUser(UserDTO user);
        //Task<bool> LoginRequest(LoginRequest loginRequest);

    }
}
