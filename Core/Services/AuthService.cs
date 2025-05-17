using Domain;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Services
{
    public class AuthService(UserManager<User> _userManager, IOptions<JwtOptions> _options, IEmailService _emailService
        , IHttpContextAccessor _httpContextAccessor, IUnitOfWork _unitOfWork) : IAuthService
    {
        public async Task<UserLoginDto> Login(LoginDto loginDto)
        {
            var _jwtOptions = _options.Value;
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnAuthorizedException();

            var checkPass = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!checkPass) throw new UnAuthorizedException();

            return new UserLoginDto
            {
                Token = await CreateTokenAsync(user),
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtOptions.expirationInDays),
                Username = user.UserName,
            };
        }
        public async Task<UserResultDto> Register(RegisterDto registerDto)
        {
            var baseUserName = registerDto.UserName ?? registerDto.Email.Split('@')[0];
            baseUserName = Regex.Replace(baseUserName, "[^a-zA-Z0-9]", "");
            var uniqueUserName = baseUserName;
            int suffix = 1;

            while (await _userManager.FindByNameAsync(uniqueUserName) != null)
            {
                uniqueUserName = $"{baseUserName}{suffix++}";
            }

            var user = new User()
            {
                UserName = uniqueUserName,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,             
            };

            try
            {
                var createUser = await _userManager.CreateAsync(user, registerDto.Password);
                // handle if create user process is successed or not
                if (!createUser.Succeeded)
                {
                    var errors = createUser.Errors.Select(e => e.Description).ToList();
                    throw new ValidationException(errors);
                }

                if (!await _userManager.IsInRoleAsync(user, registerDto.Role))
                {
                    await _userManager.AddToRoleAsync(user, registerDto.Role);
                }
                // ربط Identity User مع الـ Business Entities
                if (registerDto.Role.ToLower() == "client")
                {
                    var client = new Client
                    {
                        ClientId = Guid.NewGuid(),
                        fullName = $"{registerDto.FirstName} {registerDto.LastName}",
                        email = registerDto.Email,
                        phoneNumber = registerDto.PhoneNumber ?? "",
                        UserId = user.Id
                    };

                    await _unitOfWork.GetRepository<Client, Guid>().AddAsync(client);
                }
                else if (registerDto.Role.ToLower() == "worker")
                {
                    var worker = new Worker
                    {
                        workerId = Guid.NewGuid(),
                        fullName = $"{registerDto.FirstName} {registerDto.LastName}",
                        email = registerDto.Email,
                        phoneNumber = registerDto.PhoneNumber ?? "",
                        UserId = user.Id,
                        Experience = 0,
                        Price = 0,
                        // Type = Domain.Type.Electrician
                        // Type = default أو ممكن تضيفه في الـ DTO وتجيبه من هناك
                    };

                    await _unitOfWork.GetRepository<Worker, Guid>().AddAsync(worker);
                }
                await _unitOfWork.SaveChangesAsync();

                var roles = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "NoRole";
                return new UserResultDto(user.UserName, await CreateTokenAsync(user), user.Email, roles);
            }
            catch (Exception)
            {
                // لو حدث خطأ في إضافة العميل/العامل أو الحفظ، نحذف المستخدم في السيكيورتي لمنع التناقض
                await _userManager.DeleteAsync(user);
                throw;
            }
        }
        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
                return true;                  // الأفضل إرجاع true دائما لتجنب الكشف عن وجود البريد

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var request = _httpContextAccessor.HttpContext!.Request;
            var resetUrl = $"https://localhost:7242/ResetPassword.html?email={Uri.EscapeDataString(user.Email!)}&token={Uri.EscapeDataString(token)}";

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "Emails", "PasswordResetTemplate.html");
            var htmlTemplate = await System.IO.File.ReadAllTextAsync(templatePath);

            var body = htmlTemplate
                .Replace("{resetUrl}", resetUrl)
                .Replace("{year}", DateTime.Now.Year.ToString());

            await _emailService.SendEmailAsync(user.Email!, "Reset Your Password", body);

            return true;
        }
        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDTO dto)
        {
            var normalizedEmail = dto.Email.Trim().ToLowerInvariant();
            var user = await _userManager.FindByEmailAsync(normalizedEmail);

            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User doesn't exist." });

            var decodedToken = HttpUtility.UrlDecode(dto.Token);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, dto.NewPassword);

            return result;
        }
        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();
        }
        private async Task<string> CreateTokenAsync(User user)
        {
            var _jwtOptions = _options.Value;

            // Set Private Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email ?? "no-email@example.com"),
                new Claim("userId", user.Id.ToString())
            };

            // Get Roles To Set It Into Private Claims
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Use SecretKey instead of secretKey (if you changed the name in appsettings.json)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.secretKey));
            var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: _jwtOptions.issuer,
                audience: _jwtOptions.audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtOptions.expirationInDays),
                signingCredentials: signingCreds
            );

            // Return the JWT token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
