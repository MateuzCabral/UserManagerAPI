using Api.KmgShop.UserManager.DTOs;
using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.KmgShop.UserManager.Services.LoginUser;

public class LoginUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public LoginUserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        var tokenDescripto = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id",user.UserId.ToString()),
                new Claim("firstname",user.FirstName),
                new Claim("lastname",user.LastName),
                new Claim("email",user.Email),
                new Claim("role",user.Role.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescripto);
        return tokenHandler.WriteToken(token);
    }

    public async Task<dynamic> LoginAsync(LoginDTO loginDto)
    {
        var user = await _userRepository.VerifyEmailAsync(loginDto.Email);

        if (user == null) return null;
        
        if(!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash)) {
            return null;
        }

        var token = GenerateToken(user);
        var userWithToken = new
        {
            user,
            token,
        };

        return userWithToken;
    }
}
