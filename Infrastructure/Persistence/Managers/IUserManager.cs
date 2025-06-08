using Domain.Entities;
using Shared;

namespace Persistence.Managers
{
    public interface IUserManager
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> FindByIdAsync(string id);
        Task<IList<string>> GetUserRolesAsync(User user);
        Task<User> AddUserAsync(User user, string password);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
