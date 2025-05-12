using Microsoft.AspNetCore.Identity;
using Shared;

namespace Services.Abstraction
{
    public interface IAuthService
    {
        public Task<UserLoginDto> Login(LoginDto loginDto);
        public Task<UserResultDto> Register(RegisterDto registerDto);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDTO dto);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordDTO dto);

        Task SignOutAsync();
    }
}
