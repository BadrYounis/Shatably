using Domain.Entities;
using Persistence.Managers;
using Services.Abstraction;
using Shared;

namespace Services
{
    public class UserService(IUserManager _userManager) : IUserService
    {
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.GetAllUsersAsync();
            return users.Select(MapToUserDto);
        }
        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userManager.GetUserByIdAsync(id);
            return user is not null ? MapToUserDto(user) : null;
        }
        public async Task<UserDto> AddUserAsync(AddUserDto dto)
        {
            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                IsActive = true
            };

            var createdUser = await _userManager.AddUserAsync(user, dto.Password);
            return MapToUserDto(createdUser);
        }
        public async Task<bool> UpdateUserAsync(Guid id, UpdateUserDto dto)
        {
            var user = await _userManager.GetUserByIdAsync(id);
            if (user is null) return false;

            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.IsActive = dto.IsActive;

            return await _userManager.UpdateUserAsync(user);
        }
        public async Task<bool> DeleteUserAsync(Guid id)
            => await _userManager.DeleteUserAsync(id);
        private UserDto MapToUserDto(User user) => new UserDto
        {
            Id = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
}
