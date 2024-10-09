using SampleOneWebAPI.Models;
using SampleOneWebAPI.Repositories.Interfaces;
using SampleOneWebAPI.Services.Interfaces;

namespace SampleOneWebAPI.Services
{
    public class RoleService: IRoleService
    {

        private readonly IRoleRepository _roleRepository;
        private readonly IRepository<Role> _repository;

        public RoleService(IRoleRepository roleRepository, IRepository<Role> repository)
        {
            _roleRepository = roleRepository;
            _repository = repository;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            var roles = await _repository.GetAll();
            return roles;
        }

        public async Task<Role> GetRole(int id)
        {
            var role = await _repository.GetById(id);
            return role;
        }

        public async Task AddRole(Role role)
        {
            await _repository.Add(role);
        }

        public async Task<bool> CheckRoleExists(int id)
        {
            return await _repository.CheckEntityExists(id);
        }

        public async Task EditRole(Role role)
        {

            await _repository.Update(role);
        }

        public async Task DeleteRole(int id)
        {
            await _repository.Delete(id);
        }

    }
}
