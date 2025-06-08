using Domain;
using Domain.Contracts;
using Domain.Entities;
using Persistence.Managers;
using Services.Abstraction;
using Shared;

namespace Services
{
    public class UserService(IUserManager _userManager, IUnitOfWork _unitOfWork) : IUserService
    {
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.GetAllUsersAsync() ?? new List<User>();
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
                IsActive = true,
                Address = dto.Address is not null
                    ? new Address
                    {
                        city = dto.Address.city,
                        region = dto.Address.region,
                        street = dto.Address.street
                    }
                    : null
            };

            var createdUser = await _userManager.AddUserAsync(user, dto.Password);
            return MapToUserDto(createdUser);
        }

        //public async Task<bool> UpdateUserAsync(Guid id, UpdateUserDto dto)
        //{
        //    var user = await _userManager.GetUserByIdAsync(id);
        //    if (user is null) return false;
        //
        //    user.UserName = dto.UserName;
        //    user.Email = dto.Email;
        //    user.FirstName = dto.FirstName;
        //    user.LastName = dto.LastName;
        //    user.IsActive = dto.IsActive;
        //
        //    return await _userManager.UpdateUserAsync(user);
        //}
        public async Task<bool> UpdateUserAsync(Guid id, UpdateUserDto dto)
        {
            var user = await _userManager.GetUserByIdAsync(id);
            if (user is null) return false;

            // Update Identity User fields
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.IsActive = dto.IsActive;
            if (dto.Address is not null)
            {
                user.Address = new Address
                {
                    city = dto.Address.city,
                    region = dto.Address.region,
                    street = dto.Address.street
                };
            }

            var updateResult = await _userManager.UpdateUserAsync(user);
            if (!updateResult)
                return false;

            // Get user roles
            var roles = await _userManager.GetUserRolesAsync(user);
            if (roles.Contains("Client"))
            {
                var client = await _unitOfWork.GetRepository<Client, Guid>()
                    .FindAsync(c => c.UserId == id);
                var clientEntity = client.FirstOrDefault();

                if (clientEntity != null)
                {
                    clientEntity.fullName = $"{dto.FirstName}{dto.LastName}".Trim();
                    clientEntity.email = dto.Email;
                    _unitOfWork.GetRepository<Client, Guid>().Update(clientEntity);
                }
            }
            else if (roles.Contains("Worker"))
            {
                var worker = await _unitOfWork.GetRepository<Worker, Guid>()
                    .FindAsync(w => w.UserId == id);
                var workerEntity = worker.FirstOrDefault();

                if (workerEntity != null)
                {
                    workerEntity.fullName = $"{dto.FirstName}{dto.LastName}".Trim();
                    workerEntity.email = dto.Email;
                    _unitOfWork.GetRepository<Worker, Guid>().Update(workerEntity);
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            try
            {
                var client = await _unitOfWork.GetRepository<Client, Guid>()
                    .FindAsync(c => c.UserId == userId);
                var clientEntity = client.FirstOrDefault();

                if (clientEntity != null)
                    _unitOfWork.GetRepository<Client, Guid>().Delete(clientEntity!);

                var worker = await _unitOfWork.GetRepository<Worker, Guid>()
                    .FindAsync(w => w.UserId == userId);
                var workerEntity = worker.FirstOrDefault();

                if (workerEntity != null)
                    _unitOfWork.GetRepository<Worker, Guid>().Delete(workerEntity!);

                await _unitOfWork.SaveChangesAsync();

                var user = await _userManager.GetUserByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.DeleteUserAsync(user.Id);
                    if (!result)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //=> await _userManager.DeleteUserAsync(id);
        private UserDto MapToUserDto(User user) => new UserDto
        {
            Id = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            Address = user.Address != null ? new AddressDto
            {
                city = user.Address.city,
                region = user.Address.region,
                street = user.Address.street
            } : null
        };
    }
}
