using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;

namespace Api.KmgShop.UserManager.Services.DeleteUser;

public class DeleteUserService
{
    private readonly IUserRepository _userRepository;

    public DeleteUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> DeleteUserAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if(user == null) return null;

        await _userRepository.DeleteUserAsync(user);
        return user;
    }
}
