using Shared;

namespace Services.Abstraction;
public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task<UserDto> AddUserAsync(AddUserDto dto);
    Task<UserDto?> UpdateUserAsync(UpdateUserDto dto);
    Task<bool> DeleteUserAsync(Guid id);
}
