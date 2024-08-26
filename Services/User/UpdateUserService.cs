using Api.KmgShop.UserManager.DTOs;
using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;
using System.Security.Claims;

namespace Api.KmgShop.UserManager.Services.UpdateUser;

public class UpdateUserService
{
    private readonly IUserRepository _userRepository;

    public UpdateUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> UpdateUserAsync(UserDTO userDTO, ClaimsPrincipal userClaims)
    {
        int idUser = int.Parse(userClaims.FindFirstValue("id"));
        var user = await _userRepository.GetUserByIdAsync(idUser);
        if (idUser != user.UserId || user == null) return null;

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
