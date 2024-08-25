using Api.KmgShop.UserManager.DTOs;
using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;
using BCrypt.Net;

namespace Api.KmgShop.UserManager.Services.CreateUser;

public class CreateUserService
{
    private readonly IUserRepository _userRepository;

    public CreateUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(UserDTO userDTO)
    {
        var emailExist = await _userRepository.VerifyEmailAsync(userDTO.Email);
        if (emailExist != null) return null;

        var user = new User
        {
            FirstName = userDTO.FirstName,
            LastName = userDTO.LastName,
            Email = userDTO.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.PasswordHash)
        };
        await _userRepository.CreateUserAsync(user);
        return user;
    }
}
