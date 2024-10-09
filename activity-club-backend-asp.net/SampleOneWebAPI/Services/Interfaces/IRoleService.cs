using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRole(int id);
        Task AddRole(Role role);
        Task<bool> CheckRoleExists(int id);
        Task EditRole(Role role);
        Task DeleteRole(int id);
    }
}
