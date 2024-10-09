using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SampleOneWebAPI.DTOs.UserDTOs;
using SampleOneWebAPI.Models;
using SampleOneWebAPI.Services.Interfaces;

namespace SampleOneWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsers();
                var userDTOs = _mapper.Map<List<UserDTO>>(users);
                return Ok(userDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult> AddUser(UserDTO user)
        {
            try
            {

                await _userService.AddUser(user);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUser(id);

                if (await _userService.CheckUserExists(id) is false)
                {
                    return NotFound($"User with id {id} not found!");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                if (await _userService.CheckUserExists(id))
                {
                    await _userService.DeleteUser(id);
                    return Ok("User Deleted Successfully!");
                }
                else
                {
                    return BadRequest($"User with id {id} does not exist");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<bool>> LoginUser(UserDTO user)
        {
            var valid = await _userService.LoginUser(user);
            return valid;
        }

        //[HttpPost("Login")]
        //public async Task<ActionResult<bool>> LoginRequest(LoginRequest loginRequest)
        //{
        //    var correctLoginInfo = await _userService.LoginRequest(loginRequest);
        //    return correctLoginInfo;
        //}

    }
}
