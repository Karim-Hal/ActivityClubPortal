using Microsoft.AspNetCore.Mvc;
using SampleOneWebAPI.Models;
using SampleOneWebAPI.Services;
using SampleOneWebAPI.Services.Interfaces;


namespace SampleOneWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService; 
            
        }
        [HttpGet("GetRoles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            try
            {
                var roles = await _roleService.GetRoles();
                return Ok(roles);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddRole")]
        public async Task<ActionResult> AddRole(Role role)
        {
            try
            {
                if (role.Id == 0)
                {
                    await _roleService.AddRole(role);

                }
                else
                {
                    var checkIfExists = await _roleService.CheckRoleExists(role.Id);
                    if (checkIfExists)
                    {
                        await _roleService.EditRole(role);
                    }
                    else
                    {
                        return Ok("Role not found");
                    }
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRole/{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            try
            {
                var role = await _roleService.GetRole(id);

                if (await _roleService.CheckRoleExists(id) is false)
                {
                    return NotFound($"Role with id {id} not found!");
                }
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRole(int id)
        {
            try
            {
                if (await _roleService.CheckRoleExists(id))
                {
                    await _roleService.DeleteRole(id);

                    return Ok("Role Deleted Successfully!");
                }
                else
                {
                    return BadRequest($"Role with id {id} does not exist");
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
