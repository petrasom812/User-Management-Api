using Microsoft.AspNetCore.Mvc;
using UserSystem.Api.Dtos;
using UserSystem.Api.Services;

namespace UserSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCotroller : ControllerBase
    {
        private readonly ServiceUser _service;
        public UserCotroller(ServiceUser service)
        {
            _service = service;
        }
        [HttpGet]//Get All
        public async Task<ActionResult> GetUsers()
        {
            var getUsers = await _service.GetAllUsers();
            return Ok(getUsers);
        }
        [HttpGet("{id}")] //Get by Id
        public async Task<ActionResult> GetUserDetailById(int id)
        {
            var getUserDetail = await _service.GetUserDetailById(id);

            return getUserDetail == null ? NotFound("User not found.")
                : CreatedAtAction(nameof(GetUserDetailById), new {Id = getUserDetail.Id}, getUserDetail);
        }
        [HttpPost] // Create a new user
        public async Task<ActionResult> AddNewUser(CreateUserDto dto)
        {
            var addNewUser = await _service.AddNewUser(dto.Email, dto.Username, dto.Role);
            return Ok(addNewUser);
        }
        [HttpPut("{id}")] // update user
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDetailDto dto)
        {
            var updateUser = await _service.UpdateUserDetail(id, dto.Username, dto.Email, dto.Role);
            return updateUser == null ? NotFound("User not found.") : Ok(updateUser);
        }
        [HttpDelete("{id}")] //Soft detele a user
        public async Task<IActionResult> SoftDeleteUser(int id)
        {
            await _service.SoftDelete(id);
            return NoContent();
        }
    }
}