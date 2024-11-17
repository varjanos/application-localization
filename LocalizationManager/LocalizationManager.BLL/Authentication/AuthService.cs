using LocalizationManager.Transfer.AuthDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using LocalizationManager.DAL.Entities;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace LocalizationManager.BLL.Authentication;

internal class AuthService(
    UserManager<User> _userManager,
    SignInManager<User> _signInManager,
    IConfiguration _configuration) : IAuthService
{
    public async Task RegisterAsync(RegisterDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = dto.Username,
            Email = dto.Email,
        };

        var createdUser = await _userManager.CreateAsync(user, dto.Password);

        if (!createdUser.Succeeded)
        {
            throw new Exception($"Registration failed, reason: {string.Join(";", createdUser.Errors.Select(x => x.Description))}");
        }
    }

    public async Task<string> LoginAsync(LoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var token = GenerateJwtToken(user);
            return token;
        }

        throw new Exception("Login failed");
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    private string GenerateJwtToken(User user)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}
