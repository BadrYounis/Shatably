using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService _userService) : ControllerBase
    {
        //GET: BaseUrl/api/Users/GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        => Ok(await _userService.GetAllUsersAsync());

        //GET: BaseUrl/api/Users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        //POST: BaseUrl/api/Users
        [HttpPost]
        public async Task<IActionResult> Create(AddUserDto dto)
        {
            var created = await _userService.AddUserAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        //PUT: BaseUrl/api/Users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserDto dto)
        {
            var success = await _userService.UpdateUserAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        //DELETE: BaseUrl/api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _userService.DeleteUserAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
