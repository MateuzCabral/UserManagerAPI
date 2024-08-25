using Api.KmgShop.UserManager.DTOs;
using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;

namespace Api.KmgShop.UserManager.Services.UpdateUser;

public class UpdateUserService
{
    private readonly IUserRepository _userRepository;

    public UpdateUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> UpdateUserAsync(int userId, UserDTO userDTO)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user != null)
        {
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            await _userRepository.UpdateUserAsync(user);
        }
        return user;
    }
}
