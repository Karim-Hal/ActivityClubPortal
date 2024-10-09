using AutoMapper;
using SampleOneWebAPI.DTOs.UserDTOs;
using SampleOneWebAPI.Models;
using SampleOneWebAPI.Repositories.Interfaces;
using SampleOneWebAPI.Services.Interfaces;

namespace SampleOneWebAPI.Services
{
    public class UserService: IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IRepository<User> repository, IMapper mapper)
        {
            _userRepository = userRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _repository.GetAll();
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _repository.GetById(id);
            return user;
        }

        public async Task AddUser(UserDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            await _repository.Add(userEntity);
        }

        public async Task<bool> CheckUserExists(int id)
        {
            return await _repository.CheckEntityExists(id);
        }

        public async Task EditUser(UserDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            await _repository.Update(userEntity);
        }

        public async Task DeleteUser(int id)
        {
            await _repository.Delete(id);
        }

        //public async Task<bool> LoginRequest(LoginRequest loginRequest)
        //{
        //   return await _userRepository.LoginRequest(loginRequest);
        //}

        public async Task<bool> LoginUser(UserDTO user)
        {
            return await _userRepository.LoginUser(user);
        }
    }
}
