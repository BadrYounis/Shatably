using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager _serviceManager) : ControllerBase
    {
        //BaseUrl/api/Auth/login
        [HttpPost("login")]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var login = await _serviceManager.AuthService.Login(loginDto);
            return Ok(login);
        }

        //BaseUrl/api/Auth/register
        [HttpPost("register")]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var register = await _serviceManager.AuthService.Register(registerDto);
            return Ok(register);
        }

        //BaseUrl/api/Auth/forgot-password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceManager.AuthService.ForgotPasswordAsync(dto);
            if (!result)
                return BadRequest("No user associated with this email.");

            return Ok("Password reset link has been sent to your email.");
        }

        //BaseUrl/api/Auth/reset-password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
                if (!ModelState.IsValid)
        return BadRequest(ModelState);

            var result = await _serviceManager.AuthService.ResetPasswordAsync(dto);

            if (!result.Succeeded)
            {
                var userNotExist = result.Errors.Any(e => e.Description == "User doesn't exist.");
                if (userNotExist)
                    return NotFound(new { message = "User doesn't exist." });

                return BadRequest(new
                {
                    message = "Password reset failed.",
                    details = result.Errors.Select(e => e.Description)
                });
            }
            return Ok(new { message = "Password has been reset successfully." });
        }

        //BaseUrl/api/Auth/signout
        [HttpPost("signout")]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await _serviceManager.AuthService.SignOutAsync();
            return Ok(new { message = "Signed Out Successfully" });
        }
    }
}
