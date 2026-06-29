using Microsoft.AspNetCore.Mvc;
using UserSystem.Api.Dtos;
using UserSystem.Api.Interface;

namespace UserSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCotroller : ControllerBase
    {
        private readonly IServiceUser _service;
        public UserCotroller(IServiceUser service)
        {
            _service = service;
        }
        [HttpGet]//Get All
        public async Task<ActionResult> GetUsers()
        {
            var getUsers = await _service.GetAllUsersAsync();
            return Ok(getUsers);
        }
        [HttpGet("{id}")] //Get by Id
        public async Task<ActionResult> GetUserDetailById(int id)
        {
            var getUserDetail = await _service.GetUserDetailByIdAsync(id);

            return getUserDetail == null ? NotFound("User not found.")
                : Ok(getUserDetail);
        }
        [HttpPost] // Create a new user
        public async Task<ActionResult> AddNewUser(CreateUserDto dto)
        {
            var addNewUser = await _service.AddNewUserAsync(dto.Email, dto.Username, dto.Role);
            return Ok(addNewUser);
        }
        [HttpPut("{id}")] // update user
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDetailDto dto)
        {
            var updateUser = await _service.UpdateUserDetailAsync(id, dto.Username, dto.Email, dto.Role);
            return updateUser == null ? NotFound("User not found.") : Ok(updateUser);
        }
        [HttpDelete("{id}")] //Soft detele a user
        public async Task<IActionResult> SoftDeleteUser(int id)
        {
            await _service.SoftDeleteAsync(id);
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> ReActivateUser(int id)
        {
            await _service.ReActivateUserAsync(id);
            return Ok("User's account has been re-activated.");
        }
        
    }
}