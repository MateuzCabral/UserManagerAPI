using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;
using System.Security.Claims;

namespace Api.KmgShop.UserManager.Services.DeleteUser;

public class DeleteUserService
{
    private readonly IUserRepository _userRepository;

    public DeleteUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> DeleteUserAsync(int userId, ClaimsPrincipal userClaims)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if(user == null) return null;

        int idUser = int.Parse(userClaims.FindFirstValue("id"));
        if (idUser != user.UserId) return null;

        await _userRepository.DeleteUserAsync(user);
        return user;
    }
}
