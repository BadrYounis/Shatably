using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Managers
{
    public class UserManager(UserManager<User> _userManager) : IUserManager
    {
        public async Task<IEnumerable<User>> GetAllUsersAsync()
            => await _userManager.Users.ToListAsync();

        public async Task<User?> GetUserByIdAsync(Guid id)
            => await _userManager.FindByIdAsync(id.ToString());
        public async Task<User> AddUserAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new Exception("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            return user;
        }
        public async Task<bool> UpdateUserAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await GetUserByIdAsync(id);
            if (user is null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

    }
}
