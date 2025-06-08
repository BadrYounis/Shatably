using Domain;
using Domain.Contracts;
using Domain.Entities;
using Persistence.Managers;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class UserService(IUserManager _userManager, IUnitOfWork _unitOfWork) : IUserService
    {
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userManager.GetAllUsersAsync() ?? new List<User>();
                return users.Select(MapToUserDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetAllUsersAsync] Exception: {ex.Message}");
                throw; // أو يمكنك هنا إرجاع قيمة افتراضية حسب المتطلبات
            }
        }
        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _userManager.GetUserByIdAsync(id);
                return user is not null ? MapToUserDto(user) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetUserByIdAsync] Exception: {ex.Message}");
                throw;
            }
        }
        public async Task<UserDto> AddUserAsync(AddUserDto dto)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"[AddUserAsync] Exception: {ex.Message}");
                throw;
            }
        }
        public async Task<UserDto?> UpdateUserAsync(UpdateUserDto dto)
        {
            var user = await _userManager.GetUserByIdAsync(dto.Id);
            if (user is null) return null;

            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.IsActive = dto.IsActive;

            if (dto.Address is not null)
            {
                user.Address ??= new Address(); // لو Address = null نهيئه

                user.Address.city = dto.Address.city;
                user.Address.region = dto.Address.region;
                user.Address.street = dto.Address.street;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result)
            {
                Console.WriteLine("[UpdateUserAsync] Failed to update user.");
                return null;
            }

            return MapToUserDto(user);
        }
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            try
            {
                var client = await _unitOfWork.GetRepository<Client, Guid>()
                    .FindAsync(c => c.UserId == userId);
                var clientEntity = client.FirstOrDefault();

                if (clientEntity != null)
                {
                    _unitOfWork.GetRepository<Client, Guid>().Delete(clientEntity);
                    Console.WriteLine("[DeleteUserAsync] Deleted Client entity");
                }

                var worker = await _unitOfWork.GetRepository<Worker, Guid>()
                    .FindAsync(w => w.UserId == userId);
                var workerEntity = worker.FirstOrDefault();

                if (workerEntity != null)
                {
                    _unitOfWork.GetRepository<Worker, Guid>().Delete(workerEntity);
                    Console.WriteLine("[DeleteUserAsync] Deleted Worker entity");
                }

                await _unitOfWork.SaveChangesAsync();
                Console.WriteLine("[DeleteUserAsync] Saved changes after deleting related entities");

                var user = await _userManager.GetUserByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.DeleteUserAsync(user.Id);
                    Console.WriteLine($"[DeleteUserAsync] DeleteUserAsync result: {result}");
                    if (!result)
                    {
                        Console.WriteLine("[DeleteUserAsync] Failed to delete user");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("[DeleteUserAsync] User not found");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DeleteUserAsync] Exception: {ex.Message}");
                throw;
            }
        }
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
